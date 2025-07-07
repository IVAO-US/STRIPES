using CIFPReader;
using SkiaSharp;
using Uno.WinUI.Graphics2DSK;
using Windows.Foundation;

namespace STRIPES.Pages;

public class ScopeCanvas : SKCanvasElement
{
	protected override void RenderOverride(SKCanvas canvas, Size area)
	{
		canvas.Clear(SKColor.Empty);

		if (ScopeData.ControlVolumes.Count == 0)
			return;

		decimal maxLat = ScopeData.ControlVolumes.Values.Max(static v => v.Max(static decimal (Coordinate c) => c.Latitude));
		decimal minLat = ScopeData.ControlVolumes.Values.Min(static v => v.Min(static decimal (Coordinate c) => c.Latitude));
		decimal maxLon = ScopeData.ControlVolumes.Values.Max(static v => v.Max(static decimal (Coordinate c) => c.Longitude));
		decimal minLon = ScopeData.ControlVolumes.Values.Min(static v => v.Min(static decimal (Coordinate c) => c.Longitude));

		maxLat += 0.25m;
		maxLon += 0.25m;
		minLat -= 0.25m;
		minLon -= 0.25m;

		float screenSize = MathF.Min((float)area.Height, (float)area.Width);

		SKPoint WorldToScreen(Coordinate coord)
		{
			float y = (float)area.Height - (float)((coord.Latitude - minLat) / (maxLat - minLat)) * screenSize;
			float x = (float)((coord.Longitude - minLon) / (maxLon - minLon)) * screenSize;
			return new(x, y);
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
