using CIFPReader;

namespace STRIPES.Services;

internal static class ScopeData
{
	public static event Action? Invalidated;
	public static Dictionary<string, Coordinate[]> ControlVolumes { get; } = [];

	private static readonly Timer _timer = new(_ => Invalidated?.Invoke(), null, 0, 250);
}
