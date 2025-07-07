using SkiaSharp;

using Windows.Foundation;
using Uno.WinUI.Graphics2DSK;

namespace STRIPES.Pages;

public class ScopeCanvas : SKCanvasElement
{
	protected override void RenderOverride(SKCanvas canvas, Size area)
	{
		canvas.Clear(SKColor.Empty);
		canvas.DrawText("Hello, world!", (float)area.Width / 2, (float)area.Height / 2, SKTextAlign.Center, SKTypeface.Default.ToFont(), new SKPaint() {
			Color = new(0xFF, 0xFF, 0xFF),
			IsStroke = false
		});
	}
}
