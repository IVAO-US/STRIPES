using STRIPES.Extensibility;
using STRIPES.Services.Protocols;

using System.Net.Http.Json;

namespace STRIPES.Services;

internal partial class OsmTileProvider
{
	/// <summary>The URL of the tilejson.json file specifying the tile server's capabilities.</summary>
	const string TILE_JSON_URL = @"https://vector.openstreetmap.org/shortbread_v1/tilejson.json";

	public string AttributionHtml { get; private set; } = "";

	public string Name { get; } = "OSM Tile Provider";

	public IOmnibarCommand.CommandTarget Target { get; } = IOmnibarCommand.CommandTarget.Window;

	private readonly HttpClient _http;
	private string _tileFormatUrl = "";
	(int Min, int Max) _zoomRange = (-1, -1);

	public OsmTileProvider(HttpClient http)
	{
		_http = http;
		Task.Run(InitialiseAsync);
	}

	/// <summary>
	/// Grabs all the metadata needed to make requests later on.
	/// </summary>
	private async Task InitialiseAsync()
	{
		if (await _http.GetFromJsonAsync<OsmTileJsonQuery>(TILE_JSON_URL) is not OsmTileJsonQuery tileJson)
			throw new FormatException("Invalid TILE JSON. Are you sure you're connected to the internet?");

		_tileFormatUrl = tileJson.tiles.FirstOrDefault()?
				.Replace("{z}", "{0}", StringComparison.InvariantCultureIgnoreCase)
				.Replace("{zoom}", "{0}", StringComparison.InvariantCultureIgnoreCase)
				.Replace("{x}", "{1}", StringComparison.InvariantCultureIgnoreCase)
				.Replace("{y}", "{2}", StringComparison.InvariantCultureIgnoreCase)
			?? throw new FormatException("The TILE JSON did not include any tile URLs.");
		_zoomRange = (tileJson.minzoom, tileJson.maxzoom);
		Quadtree.MaxZoom = tileJson.maxzoom;
		AttributionHtml = tileJson.attribution;

		// For now, we only support tile systems which zoom to 0.
		if (tileJson.minzoom is not 0)
			throw new NotImplementedException($"The requested tile system is not implemented as it only zooms out to level {tileJson.minzoom}.");

		SkiaSharp.SKPaint paint = new() {
			Color = new(0xFF, 0xFF, 0xFF),
			IsStroke = true,
			IsAntialias = true,
			StrokeWidth = 2f
		};

		ScopeData.Quadtree = new(
			0, 0, 0,
			[.. await GetQuadtreeAsync(0, 0, 0)],
			GetQuadtreeAsync
		) {
			DisplayFeature = f => f.Layer is "ocean",
			BackgroundColour = new(0x00, 0x00, 0x00),
			FeaturePaint = _ => paint
		};
	}

	private readonly Dictionary<string, Tile> _tileCache = [];
	/// <summary>
	/// Downloads (or retrieves from cache) a raw vector tile from OpenStreetMap.
	/// </summary>
	public async Task<Tile> GetTileAsync(int zoom, int x, int y)
	{
		if (zoom < _zoomRange.Min || zoom > _zoomRange.Max)
			throw new ArgumentOutOfRangeException(nameof(zoom));

		string url = string.Format(_tileFormatUrl, zoom, x, y);

		if (_tileCache.TryGetValue(url, out var cacheRes))
			return cacheRes;

		var mvtBytes = await _http.GetByteArrayAsync(url);
		Tile res = Tile.Parser.ParseFrom(mvtBytes);
		_tileCache[url] = res;
		return res;
	}

	public async Task<IEnumerable<OsmFeature>> GetQuadtreeAsync(int zoom, int x, int y) =>
		(await GetTileAsync(zoom, x, y))
			.Layers
			.SelectMany(layer => layer.GetFeatures(zoom, x, y));
}

#pragma warning disable CS8618 // Not initialised
#pragma warning disable IDE1006 // Naming conventions
internal class OsmTileJsonQuery
{
	public string attribution { get; set; }
	public string description { get; set; }
	public int maxzoom { get; set; }
	public int minzoom { get; set; }
	public string name { get; set; }
	public string scheme { get; set; }
	public string tilejson { get; set; }
	public string[] tiles { get; set; }
	public Vector_Layers[] vector_layers { get; set; }
}

internal class Vector_Layers
{
	public string description { get; set; }
	public Fields fields { get; set; }
	public string id { get; set; }
	public int maxzoom { get; set; }
	public int minzoom { get; set; }
}

internal class Fields
{
	public string kind { get; set; }
	public string way_area { get; set; }
	public string name { get; set; }
	public string name_de { get; set; }
	public string name_en { get; set; }
	public string bridge { get; set; }
	public string tunnel { get; set; }
	public string admin_level { get; set; }
	public string disputed { get; set; }
	public string maritime { get; set; }
	public string population { get; set; }
	public string housename { get; set; }
	public string housenumber { get; set; }
	public string bicycle { get; set; }
	public string horse { get; set; }
	public string link { get; set; }
	public string oneway { get; set; }
	public string oneway_reverse { get; set; }
	public string rail { get; set; }
	public string service { get; set; }
	public string surface { get; set; }
	public string tracktype { get; set; }
	public string _ref { get; set; }
	public string ref_cols { get; set; }
	public string ref_rows { get; set; }
}
#pragma warning restore
