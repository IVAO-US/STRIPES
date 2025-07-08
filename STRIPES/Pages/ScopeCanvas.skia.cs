using CIFPReader;

using SkiaSharp;

using System.Reflection;

using Uno.WinUI.Graphics2DSK;

using Windows.Foundation;

namespace STRIPES.Pages;

public class ScopeCanvas : SKCanvasElement
{
	decimal _maxLatitude;
	decimal _minLatitude;
	decimal _maxLongitude;
	decimal _minLongitude;

	public void SetBounds(decimal minLat, decimal maxLat, decimal minLon, decimal maxLon)
	{
		(_minLatitude, _maxLatitude) = (minLat, maxLat);
		(_minLongitude, _maxLongitude) = (minLon, maxLon);

		if (IsLoaded)
			Invalidate();
	}

	public void CopyBounds(ScopeCanvas other) =>
		other.SetBounds(_minLatitude, _maxLatitude, _minLongitude, _maxLongitude);

	protected override void RenderOverride(SKCanvas canvas, Size area)
	{
		canvas.Clear(SKColor.Empty);

		if (ScopeData.ControlVolumes.Count == 0)
			return;

		float screenSize = MathF.Min((float)area.Height, (float)area.Width),
			  xOffset = ((float)area.Width - screenSize) / 2,
			  yOffset = ((float)area.Height - screenSize) / 2;

		float dLat = (float)(_maxLatitude - _minLatitude);
		float dLon = (float)(_maxLongitude - _minLongitude);

		SKPoint WorldToScreen(Coordinate coord)
		{
			float y = (float)area.Height - (float)(coord.Latitude - _minLatitude) / dLat * screenSize;
			float x = (float)(coord.Longitude - _minLongitude) / dLon * screenSize;
			return new(x + xOffset, y + yOffset);
		}

		SKPath controlVolumes = new();
		foreach (var volume in ScopeData.ControlVolumes)
		{
			bool first = true;

			foreach (Coordinate coord in volume.Value)
			{
				if (first)
					controlVolumes.MoveTo(WorldToScreen(coord));
				else
					controlVolumes.LineTo(WorldToScreen(coord));

				first = false;
			}
		}

		canvas.DrawPath(controlVolumes, new() {
			Color = new(0x13, 0x1C, 0X27)
		});

		canvas.DrawText($"Loaded Positions: {string.Join(' ', ScopeData.ControlVolumes.Keys)}", (float)area.Width / 2, (float)area.Height / 2, SKTextAlign.Center, SKTypeface.Default.ToFont(), new SKPaint() {
			Color = new(0xFF, 0xFF, 0xFF),
			IsStroke = false
		});
	}
}
