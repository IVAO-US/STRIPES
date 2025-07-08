using CIFPReader;

using Microsoft.Kiota.Abstractions.Serialization;

using STRIPES.Services.Endpoints.Models;

using System.Text.Json;

namespace STRIPES.Pages;

internal partial class ScopePage : Page
{
	public ScopeModel Model => ((ScopeViewModel)DataContext).Model;

	public ScopePage()
	{
		InitializeComponent();
		ScopeData.Invalidated += ScpBackground.Invalidate;
		AsbOmnibar.Focus(FocusState.Keyboard);
	}

	private void Page_GotFocus(object sender, RoutedEventArgs e) => AsbOmnibar.Focus(FocusState.Keyboard);
}

internal partial record ScopeModel(IvaoApiService IvaoApi, IvanConnectionService Ivan, OmnibarService Omnibar)
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
