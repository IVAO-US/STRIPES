using SkiaSharp;

using STRIPES.Extensibility;

using Uno.WinUI.Graphics2DSK;

using Windows.Foundation;

namespace STRIPES.Pages;

public class ScopeCanvas : SKCanvasElement
{
	decimal _maxLatitude = Quadtree.WEB_MERCATOR_MAX_LAT;
	decimal _minLatitude = -Quadtree.WEB_MERCATOR_MAX_LAT;
	decimal _maxLongitude = 180;
	decimal _minLongitude = -180;

	public void SetBounds(decimal minLat, decimal maxLat, decimal minLon, decimal maxLon)
	{
		(_minLatitude, _maxLatitude) = (minLat, maxLat);
		(_minLongitude, _maxLongitude) = (minLon, maxLon);

		try
		{
			Invalidate();
		}
		catch (NullReferenceException) { } // Loading isn't finished yet.
	}

	const float INV_TAU = 1 / MathF.Tau;
	const float DEG_TO_RAD = MathF.Tau / 360;

	/// <summary>Web mercator</summary>
	static SKPoint? WorldToScreen(Coordinate coord, Size area)
	{
		float screenSize = MathF.Min((float)area.Height, (float)area.Width),
			  xOffset = ((float)area.Width - screenSize) / 2,
			  yOffset = ((float)area.Height - screenSize) / 2;

		float radLat = (float)coord.Latitude * DEG_TO_RAD,
			  radLon = (float)coord.Longitude * DEG_TO_RAD;

		float x = (radLon + MathF.PI) * INV_TAU,
			  y = (MathF.PI - MathF.Log(MathF.Tan(MathF.PI * 0.25f + radLat * 0.5f))) * INV_TAU;

		x *= screenSize;
		y *= screenSize;

		x += xOffset;
		y += yOffset;

		// Clamp the values.
		if (x < 0 || x > area.Width)
			return null;
		else if (y < 0 || y > area.Height)
			return null;

		return new(x, y);
	}

	public void CopyBounds(ScopeCanvas other) =>
		other.SetBounds(_minLatitude, _maxLatitude, _minLongitude, _maxLongitude);

	protected override async void RenderOverride(SKCanvas canvas, Size area)
	{
		canvas.Clear(SKColor.Empty);

		if (ScopeData.ControlVolumes.Count is not 0)
			RenderPolys(canvas, area, new() {
				Color = new(0x13, 0x1C, 0X27, 0x55)
			}, ScopeData.ControlVolumes.Values);

		if (ScopeData.Quadtree.Zoom is not -1)
		{
			int renderArea = Math.Min((int)area.Width, (int)area.Height);

			Quadtree outerTile = await ScopeData.Quadtree.GetTileAsync(Quadtree.FindTile(_minLatitude, _maxLatitude, _minLongitude, _maxLongitude));

			if (outerTile.Zoom == Quadtree.MaxZoom)
				outerTile.Render(canvas, new(0, 0, renderArea, renderArea));
			else
			{
				(await outerTile.TopLeft).Render(canvas, new(0, 0, renderArea / 2, renderArea / 2));
				(await outerTile.BottomLeft).Render(canvas, new(0, renderArea / 2, renderArea / 2, renderArea));
				(await outerTile.TopLeft).Render(canvas, new(renderArea / 2, 0, renderArea, renderArea / 2));
				(await outerTile.TopLeft).Render(canvas, new(renderArea / 2, renderArea / 2, renderArea, renderArea));
			}
		}
	}

	private static void RenderPolys(SKCanvas canvas, Size area, SKPaint paint, params IEnumerable<Coordinate[]> polys)
	{
		decimal minLon = 180, maxLon = -180, minLat = 90, maxLat = -90;
		double minX = area.Width * 100, maxX = 0, minY = area.Height * 100, maxY = 0;

		SKPath path = new();
		foreach (var poly in polys)
		{
			bool first = true;

			foreach (Coordinate coord in poly)
			{
				if (WorldToScreen(coord.Normalised(), area) is not SKPoint endpoint)
					continue;

				if (!((double.IsNormal(endpoint.X) || endpoint.X is 0) && (double.IsNormal(endpoint.Y) || endpoint.Y is 0)))
					throw new Exception();

				minLon = Math.Min(coord.Longitude, minLon);
				maxLon = Math.Max(coord.Longitude, maxLon);
				minLat = Math.Min(coord.Latitude, minLat);
				maxLat = Math.Max(coord.Latitude, maxLat);
				minX = Math.Min(endpoint.X, minX);
				maxX = Math.Max(endpoint.X, maxX);
				minY = Math.Min(endpoint.Y, minY);
				maxY = Math.Max(endpoint.Y, maxY);

				if (first)
					path.MoveTo(endpoint);
				else
					path.LineTo(endpoint);

				first = false;
			}
		}

		System.Diagnostics.Debug.WriteLine($"Drawing poly of {polys.Sum(p => p.Length)} points in ({minLon:0.00} = {minX:0} to {maxLon:0.00} = {maxX:0}), ({minLat:0.00} = {maxY:0} to {maxLat:0.00} = {minY:0})");

		canvas.DrawPath(path, paint);
	}
}
