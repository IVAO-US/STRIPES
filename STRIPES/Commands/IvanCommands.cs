using STRIPES.Commands.WindowManagement;
using STRIPES.Extensibility;
using STRIPES.Services.Endpoints.Models;

using System.Text.RegularExpressions;

namespace STRIPES.Commands;

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

			if (await IvaoApi.GetWebeyeShapeAsync(Ivan.Callsign) is not (string atcPos, Coordinate[] coordList))
				return;

			ScopeData.BeginUpdate();
			ScopeData.ControlVolumes.Clear();
			ScopeData.ControlVolumes[atcPos] = coordList;

			foreach (BaseSessionDto atc in (await IvaoApi.GetOnlineAtcAsync()) ?? [])
			{
				if (!(atc.Callsign?.StartsWith('K') ?? false) || await IvaoApi.GetWebeyeShapeAsync(atc.Callsign) is not (string callsign, Coordinate[] coords))
					continue;

				ScopeData.ControlVolumes[callsign] = [.. coords];
			}

			var (minLat, maxLat) = (Math.Clamp(coordList.Min(static c => c.Latitude), -85m, 85m), Math.Clamp(coordList.Max(static c => c.Latitude), -85m, 85m));
			var (minLon, maxLon) = (Math.Clamp(coordList.Min(static c => c.Longitude), -180m, 180m), Math.Clamp(coordList.Max(static c => c.Longitude), -180m, 180m));

			if (ApplicationHelper.Windows.GetCanvasByWindow(".") is ScopeCanvas mainScope)
				mainScope.SetBounds(minLat, maxLat, minLon, maxLon);

			foreach (ScopeCanvas canvas in ApplicationHelper.Windows.Where(static w => w.Content is ScopeCanvas).Select(static w => w.Content).Cast<ScopeCanvas>())
				canvas.SetBounds(minLat, maxLat, minLon, maxLon);

			ScopeData.EndUpdate();
			Tooltip.Notify("Polygon loading complete!");
		}
		else
			Tooltip.Notify(authMessage);
	}
}
