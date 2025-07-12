using SkiaSharp;

using System.Collections.Immutable;

namespace STRIPES.Extensibility;

public record Quadtree(int Zoom, int X, int Y, ImmutableArray<OsmFeature> Features, Quadtree.QuadtreeLoader GetChildAsync)
{
	public static readonly decimal WEB_MERCATOR_MAX_LAT = 85.0511287798065893639432033524m;

	public static int MaxZoom { get; set; } = 0;

	Quadtree? _topLeft = null,
			  _topRight = null,
			  _bottomLeft = null,
			  _bottomRight = null;

	/// <summary>Pulls from the cache if possible, otherwise generates the tile.</summary>
	private async Task<Quadtree> WrapReturnAsync(Quadtree? target, int xOffset, int yOffset, Action<Quadtree> assignment)
	{
		if (target is null)
		{
			int z = Zoom + 1,
				x = X * 2 + xOffset,
				y = Y * 2 + yOffset;

			ImmutableArray<OsmFeature> features = [.. await GetChildAsync(z, x, y)];
			target = this with {
				X = x,
				Y = y,
				Zoom = z,
				Features = features
			};
			assignment(target);
		}

		return target;
	}

	public Task<Quadtree> TopLeft => WrapReturnAsync(_topLeft, 0, 0, q => _topLeft = q);
	public Task<Quadtree> TopRight => WrapReturnAsync(_topRight, 1, 0, q => _topRight = q);
	public Task<Quadtree> BottomLeft => WrapReturnAsync(_bottomLeft, 0, 1, q => _bottomLeft = q);
	public Task<Quadtree> BottomRight => WrapReturnAsync(_bottomRight, 1, 1, q => _bottomRight = q);

	public Coordinate TopLeftCoordinate { get; } = Coordinate.TileTopLeft(Zoom, X, Y);

	private Func<OsmFeature, bool> _displayFeature = _ => true;
	private Func<OsmFeature, SKPaint> _featurePaint = _ => new() { Color = new(0xFF, 0xFF, 0xFF), IsStroke = true };
	private SKColor _backgroundColour = new(0, 0, 0);

	/// <returns><see langword="true"/> if the feature should be rendered.</returns>
	public Func<OsmFeature, bool> DisplayFeature { private get => _displayFeature; set { _displayFeature = value; Render(); } }

	/// <summary>Given an <see cref="OsmFeature"/>, returns the <see cref="SKPaint"/> that should be used to render it.</summary>
	public Func<OsmFeature, SKPaint> FeaturePaint { private get => _featurePaint; set { _featurePaint = value; Render(); } }

	public SKColor BackgroundColour { private get => _backgroundColour; set { _backgroundColour = value; Render(); } }

	/// <summary>
	/// For a given set of tile coordinates, get the appropriate <see cref="Quadtree"/>.
	/// </summary>
	public Task<Quadtree> GetTileAsync((int Zoom, int X, int Y) tile) => GetTileAsync(tile.Zoom, tile.X, tile.Y);

	/// <summary>
	/// For a given set of tile coordinates, get the appropriate <see cref="Quadtree"/>.
	/// </summary>
	public async Task<Quadtree> GetTileAsync(int zoom, int x, int y)
	{
		ArgumentOutOfRangeException.ThrowIfGreaterThan(zoom, MaxZoom, nameof(zoom));

		if (zoom == Zoom)
		{
			ArgumentOutOfRangeException.ThrowIfNotEqual(x, X, nameof(x));
			ArgumentOutOfRangeException.ThrowIfNotEqual(y, Y, nameof(y));
			return this;
		}

		// Slowly move the x/y into one deeper than current to figure out where to go.
		int xCheck = x, yCheck = y;
		for (int zoomCheck = zoom - 1; zoomCheck > Zoom; --zoomCheck)
			(xCheck, yCheck) = (xCheck / 2, yCheck / 2);

		bool left = xCheck % 2 is 0,
			 top = yCheck % 2 is 0;

		if (left && top)
			return await (await TopLeft).GetTileAsync(zoom, x, y);
		else if (left)
			return await (await BottomLeft).GetTileAsync(zoom, x, y);
		else if (top)
			return await (await TopRight).GetTileAsync(zoom, x, y);
		else
			return await (await BottomRight).GetTileAsync(zoom, x, y);
	}

	/// <summary>
	/// Determines the highest zoom level at which two lat/lon bounds fall within the same tile.
	/// </summary>
	/// <returns>The zoom level and tile coordinates for that shared tile.</returns>
	public static (int Zoom, int X, int Y) FindTile(decimal minLat, decimal maxLat, decimal minLon, decimal maxLon)
	{
		float lat1 = (float)minLat,
			  lat2 = (float)maxLat,
			  lon1 = (float)minLon,
			  lon2 = (float)maxLon;

		// Finds the tile coordinates of a given point at a given zoom.
		static (int X, int Y) GetTileIndices(float lat, float lon, int zoom)
		{
			float latRad = lat * DEG_TO_RAD;
			int n = 1 << zoom;

			int x = (int)((lon + 180f) / 360f * n),
				y = (int)((1f - MathF.Log(MathF.Tan(latRad) + 1f / MathF.Cos(latRad)) / MathF.PI) * 0.5f * n);

			return (
				x == n ? x - 1 : x,
				y == n ? y - 1 : y
			);
		}

		// Work back from the maximum zoom level until we find a tile that both points fit on.
		for (int z = 1; z <= MaxZoom; ++z)
		{
			var bottomLeftTile = GetTileIndices(lat1, lon1, z);
			var topRightTile = GetTileIndices(lat2, lon2, z);

			if (bottomLeftTile != topRightTile)
			{
				int fallbackZoom = z - 1;
				var tile = GetTileIndices(lat1, lon1, fallbackZoom);
				return (fallbackZoom, tile.X, tile.Y);
			}
		}

		// Made it out. Looks like we're good all the way down to max zoom!
		var maxTile = GetTileIndices(lat1, lon1, MaxZoom);
		return (MaxZoom, maxTile.X, maxTile.Y);
	}

	/// <summary>
	/// Finds the web mercator coordinates for a given lat/lon pair.
	/// </summary>
	public static (float X, float Y) CoordToWebMercator(decimal latitude, decimal longitude)
	{
		// The radius parameter from web mercator.
		float radius = 6378137f,
			  xRad = (float)longitude * DEG_TO_RAD,
			  yRad = (float)latitude * DEG_TO_RAD;

		// Convert everything to web mercator coordinates.
		float x = radius * xRad,
			  y = radius * MathF.Log(MathF.Tan(MathF.PI * 0.25f + yRad * 0.5f));

		return (x, y);
	}

	private bool _renderComplete = false;
	public void Render(SKCanvas target, SKRect area)
	{
		if (!_renderComplete || _renderSurface.Snapshot() is not SKImage image)
		{
			Render();
			Render(target, area);
			return;
		}

		SKData data = image.Encode(SKEncodedImageFormat.Png, 12);

		using (FileStream tmp = File.OpenWrite(@"C:\Users\westo\Downloads\test.png"))
			data.SaveTo(tmp);

		// This is the only way I've found to ensure it's initialised…
		if (target.LocalClipBounds.Left >= -10000)
			target.DrawImage(image, area);
	}

	const int TILE_SIZE = 2048;
	private readonly SKSurface _renderSurface = SKSurface.Create(new SKImageInfo(TILE_SIZE, TILE_SIZE));
	private void Render()
	{
		_renderSurface.Canvas.Clear(BackgroundColour);

		SKPath path = new();
		SKPaint paint = new();
		foreach (OsmFeature feature in Features)
		{
			if (!DisplayFeature(feature))
				continue;

			SKPaint newPaint = FeaturePaint(feature);
			if (newPaint != paint)
			{
				if (path.PointCount > 0)
				{
					_renderSurface.Canvas.DrawPath(path, paint);
					path = new();
				}

				paint = newPaint;
			}

			SKPoint lastJump = SKPoint.Empty;
			SKPoint tilePoint = SKPoint.Empty;

			foreach (OsmGeoCommand cmd in feature.Geometry)
				switch (cmd)
				{
					case OsmGeoCommand.MoveTo mt:
						tilePoint = new(tilePoint.X + mt.X, tilePoint.Y + mt.Y);
						path.MoveTo(tilePoint);
						lastJump = tilePoint;
						break;

					case OsmGeoCommand.LineTo lt:
						tilePoint = new(tilePoint.X + lt.X, tilePoint.Y + lt.Y);

						if (tilePoint.X is 0 or TILE_SIZE || tilePoint.Y is 0 or TILE_SIZE)
							path.MoveTo(tilePoint);
						else
							path.LineTo(tilePoint);
						break;

					case OsmGeoCommand.ClosePath _:
						path.LineTo(lastJump);
						break;

					default: throw new NotImplementedException();
				}
		}

		if (path.PointCount is not 0)
			_renderSurface.Canvas.DrawPath(path, paint);

		_renderComplete = true;
	}

	const float DEG_TO_RAD = MathF.Tau / 360;

	/// <summary>Copy a <see cref="Quadtree"/> and preserve its interior tiles.</summary>
	/// <param name="other">The <see cref="Quadtree"/> to copy.</param>
	protected Quadtree(Quadtree other) : base()
	{
		_renderSurface = SKSurface.Create(new SKImageInfo(TILE_SIZE, TILE_SIZE));

		(Zoom, X, Y, Features, GetChildAsync) = (other.Zoom, other.X, other.Y, other.Features, other.GetChildAsync);
		(_topLeft, _topRight, _bottomLeft, _bottomRight) = (other._topLeft, other._topRight, other._bottomLeft, other._bottomRight);
		(_displayFeature, _featurePaint, _backgroundColour) = (other.DisplayFeature, other.FeaturePaint, other.BackgroundColour);
		Render();
	}

	public static Quadtree Default => new(-1, 0, 0, [], (_, _, _) => throw new InvalidOperationException());

	public delegate Task<IEnumerable<OsmFeature>> QuadtreeLoader(int zoom, int x, int y);
}
