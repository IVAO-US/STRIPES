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

	/// <summary>
	/// Gets the <see cref="ATCPositionDto"/> for the specified <paramref name="callsign"/>. 
	/// </summary>
	/// <param name="callsign">Ex. KORD_TWR</param>
	/// <returns>The <see cref="ATCPositionDto"/> if it was found, otherwise <see langword="null"/>.</returns>
	public async Task<ATCPositionDto?> GetAtcPositionAsync(string callsign)
	{
		if (!await EnsureAuthenticatedAsync())
			throw new Exception("Authentication failed.");

		try
		{
			var result = await _client.V2.ATCPositions[callsign].GetAsync();
			return result;
		}
		catch (SwaggerResponsesDto)
		{
			return null;
		}
	}
}
