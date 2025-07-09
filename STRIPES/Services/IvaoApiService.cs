using STRIPES.Extensibility;
using STRIPES.Services.Endpoints.Models;

using System.Diagnostics.CodeAnalysis;

namespace STRIPES.Services;

public class IvaoApiService(IAuthenticationService _auth, IvaoApiClient _client)
{
	UserResponseDto? _me = null;

	[MemberNotNullWhen(true, nameof(_me))]
	public async ValueTask<bool> EnsureAuthenticatedAsync()
	{
		if (_me is not null)
			return true;

		if (await _auth.IsAuthenticated())
			await _auth.RefreshAsync();
		else
			await _auth.LoginAsync();

		if (await _client.V2.Users["me"].GetAsync() is not UserResponseDto user)
			return false;

		_me = user;
		return true;
	}

	/// <summary>
	/// Checks if a user can connect to a position. 
	/// </summary>
	/// <param name="position">The callsign of the position.</param>
	/// <param name="vid">The user attempting to connect. If <see langword="null"/>, the authenticated user.</param>
	/// <returns>If the user is authorized and the message from the auth server.</returns>
	public async ValueTask<(bool Authorised, string Message)> FraCheckAsync(string position, double? vid = null)
	{
		if (!await EnsureAuthenticatedAsync())
			throw new Exception("Authentication failed.");

		vid ??= _me!.Id;

		if (vid is null)
			throw new ArgumentNullException(nameof(vid));

		try
		{
			var fraRes = await _client.V2.Fras.Check[position][vid.Value].GetAsync();
			return (true, fraRes?.Message ?? throw new Exception());
		}
		catch (ErrorFraCheckDTO ex)
		{
			return (false, ex.Message);
		}
		catch (SwaggerResponsesDto ex)
		{
			return (false, ex.Message);
		}
	}

	private readonly Dictionary<string, (DateTimeOffset Cached, AtcInfo Value)> _atcPositionApiCache = [];

	/// <summary>
	/// Gets the <see cref="ATCPositionDto"/> for the specified <paramref name="callsign"/>. 
	/// </summary>
	/// <param name="callsign">Ex. KORD_TWR</param>
	/// <returns>The <see cref="ATCPositionDto"/> if it was found, otherwise <see langword="null"/>.</returns>
	public async Task<AtcInfo?> GetAtcPositionAsync(string callsign)
	{
		callsign = callsign.Trim().ToUpperInvariant();
		if (_atcPositionApiCache.TryGetValue(callsign, out var cachedVal))
		{
			// Found it in the cache. Make sure it's fresh enough.
			if (DateTimeOffset.UtcNow - cachedVal.Cached <= TimeSpan.FromMinutes(5))
				return cachedVal.Value;

			_atcPositionApiCache.Remove(callsign);
		}

		if (!await EnsureAuthenticatedAsync())
			throw new Exception("Authentication failed.");

		string facilityType = callsign.Split('_')[^1];

		try
		{
			AtcInfo result;
			if (facilityType is "CTR" or "FSS")
			{
				if (await _client.V2.Subcenters[callsign].GetAsync() is not BaseSubcenterInterfaces apiRes)
					return null;

				result = new(apiRes);
			}
			else if (facilityType is "DEP" or "GND" or "TWR" or "APP" or "DEP")
			{
				if (await _client.V2.ATCPositions[callsign].GetAsync() is not ATCPositionDto apiRes)
					return null;

				result = new(apiRes);
			}
			else
				return null;

			_atcPositionApiCache[callsign] = (DateTimeOffset.UtcNow, result);
			return result;
		}
		catch (SwaggerResponsesDto)
		{
			return null;
		}
	}

	private readonly Dictionary<string, Coordinate[]> _webeyeCache = [];

	/// <summary>
	/// Gets the webeye shape for the specified <paramref name="atcCallsign"/>
	/// </summary>
	/// <param name="atcCallsign">Ex. KORD_TWR</param>
	public async Task<(string NormalizedCallsign, Coordinate[] Boundary)?> GetWebeyeShapeAsync(string atcCallsign)
	{
		atcCallsign = atcCallsign.Trim().ToUpperInvariant();
		if (_webeyeCache.TryGetValue(atcCallsign, out var cacheVal))
			return (atcCallsign, cacheVal);

		if (await GetAtcPositionAsync(atcCallsign) is not AtcInfo atcPos)
			return null;

		if (atcPos.WebeyeShape is not Coordinate[] coordList)
			return null;

		atcCallsign = atcPos.Callsign ?? atcCallsign;

		if (coordList.Length is 0)
			// Doesn't have a shape. Just drop a dot on the centre.
			coordList = [atcPos.Centerpoint];

		_webeyeCache[atcCallsign] = coordList;

		return (atcCallsign, coordList);
	}

	public async Task<IEnumerable<BaseSessionDto>?> GetOnlineAtcAsync()
	{
		if (await _client.V2.Tracker.Whazzup.GetAsync() is not WhazzupDto whazzup || whazzup.Clients is null || whazzup.Clients.Atcs is not List<BaseSessionDto> atcSessions)
			return null;

		return atcSessions;
	}
}

public sealed record AtcInfo(string Callsign, string RadiotelephonyIdentifier, decimal Frequency, Coordinate Centerpoint, Coordinate[]? WebeyeShape)
{
	public AtcInfo(BaseSubcenterInterfaces subcenter) : this(
		subcenter.ComposePosition!,
		subcenter.AtcCallsign ?? "",
		(decimal)subcenter.Frequency!,
		new((decimal)subcenter.Latitude!, (decimal)subcenter.Longitude!),
		[.. subcenter.RegionMap?.Select(pair => new Coordinate((decimal)pair.Lat!, (decimal)pair.Lng!)) ?? []]
	)
	{ }

	public AtcInfo(ATCPositionDto atcPosition) : this(
		atcPosition.ComposePosition!,
		atcPosition.AtcCallsign ?? "",
		(decimal)atcPosition.Frequency!,
		new((decimal)atcPosition.Airport!.Latitude!, (decimal)atcPosition.Airport.Longitude!),
		[.. atcPosition.RegionMap?.Select(pair => new Coordinate((decimal)pair.Lat!, (decimal)pair.Lng!)) ?? []]
	)
	{ }
}
