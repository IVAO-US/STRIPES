using CIFPReader;

using Microsoft.Kiota.Abstractions.Serialization;

using STRIPES.Commands.WindowManagement;
using STRIPES.Extensibility;
using STRIPES.Services.Endpoints.Models;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace STRIPES.Services;

internal sealed class IvanConnectionService
{
	public string? Callsign { get; private set; }

	[MemberNotNullWhen(true, nameof(Callsign))]
	public bool IsConnected => Callsign is not null;

	/// <summary>
	/// Connects the authed user to the IVAO network (IVAN).
	/// </summary>
	/// <param name="callsign">The position to connect to.</param>
	/// <exception cref="InvalidOperationException">The user is already connected.</exception>
	[MemberNotNull(nameof(Callsign))]
	public Task ConnectAsync(string callsign)
	{
		if (IsConnected)
			throw new InvalidOperationException("Already connected to IVAN.");

		Callsign = callsign;
		return Task.CompletedTask;
	}

	/// <summary>
	/// Disconnects the user from the IVAO network (IVAN).
	/// </summary>
	/// <param name="callsign">The position to connect to.</param>
	public Task DisconnectAsync()
	{
		if (IsConnected)
			Callsign = null;

		return Task.CompletedTask;
	}
}

internal sealed partial record IvanCommands(IvaoApiService IvaoApi, IvanConnectionService Ivan, ITooltipNotifier Tooltip) : IOmnibarCommand
{
	public string Name => "Manages the connection with the network.";

	public IOmnibarCommand.CommandTarget Target => IOmnibarCommand.CommandTarget.Window;

	[GeneratedRegex(@"^OPEN (?<pos>[A-Z0-9]{4}(_[A-Z]{1,3})?_(DEL|GND|TWR|APP|DEP|CTR|FSS))$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase)]
	public partial Regex GetCommandRegex();

	public IEnumerable<string> GetSuggestions(string input) => IOmnibarCommand.PrefixSuggestions(input, "OPEN ?");

	public async Task ProcessCommandAsync(string target, Match input)
	{
		if (!input.Groups.TryGetValue("pos", out var posGroup))
			return;

		string pos = posGroup.Value.ToUpperInvariant();
		(bool authed, string authMessage) = await IvaoApi.FraCheckAsync(pos);

		if (authed)
		{
			if (Ivan.IsConnected)
				await Ivan.DisconnectAsync();

			await Ivan.ConnectAsync(pos);
			Tooltip.Notify($"Connected as {pos}. Loading polygons…");

			if (await IvaoApi.GetAtcPositionAsync(Ivan.Callsign) is not ATCPositionDto atcPos)
				return;

			if (!atcPos.AdditionalData.TryGetValue("regionMapPolygon", out var accessor) || accessor is not UntypedArray coordArray)
				return;

			List<Coordinate> coordList = [];
			foreach (decimal[] coord in JsonSerializer.Deserialize<decimal[][]>(await KiotaJsonSerializer.SerializeAsStringAsync(coordArray))!)
				coordList.Add(new Coordinate(coord[1], coord[0]));

			ScopeData.ControlVolumes[atcPos.ComposePosition ?? Ivan.Callsign] = [.. coordList];
			Tooltip.Notify("Polygon loading complete!");
		}
		else
			Tooltip.Notify(authMessage);
	}
}
