using CIFPReader;

using Microsoft.Kiota.Abstractions.Serialization;

using STRIPES.Services.Endpoints.Models;

using System.Text.Json;

namespace STRIPES.Pages;

internal partial class ScopePage : Page
{
	public ScopePage()
	{
		InitializeComponent();
		ScopeData.Invalidated += ScpBackground.Invalidate;
		AsbOmnibar.Focus(FocusState.Keyboard);
	}

	private void SpawnButton_Click(object sender, RoutedEventArgs e)
	{
		// Create and launch a window.
		ScopeCanvas childScope = new();
		ScopeData.Invalidated += () => {
			try { childScope.Invalidate(); }
			catch { }
		};

		Window window = new() {
			Content = childScope
		};

		window.Closed += (_, _) => window.Content = null;
		window.Activate();
	}

	private void Page_GotFocus(object sender, RoutedEventArgs e) => AsbOmnibar.Focus(FocusState.Keyboard);
}

internal partial record ScopeModel(IvaoApiService IvaoApi, IvanConnectionService Ivan)
{
	public async Task LoadControlVolumesAsync()
	{
		if (!Ivan.IsConnected)
			return;

		if (await IvaoApi.GetAtcPositionAsync(Ivan.Callsign) is not ATCPositionDto atcPos)
			return;

		if (!atcPos.AdditionalData.TryGetValue("regionMapPolygon", out var accessor) || accessor is not UntypedArray coordArray)
			return;

		List<Coordinate> coordList = [];
		foreach (decimal[] coord in JsonSerializer.Deserialize<decimal[][]>(await KiotaJsonSerializer.SerializeAsStringAsync(coordArray))!)
			coordList.Add(new Coordinate(coord[1], coord[0]));

		ScopeData.ControlVolumes[atcPos.ComposePosition ?? Ivan.Callsign] = [.. coordList];
	}
}
