using CIFPReader;

using STRIPES.Extensibility;

namespace STRIPES.Services;

internal static class ScopeData
{
	public static event Action? Invalidated;
	public static IDictionary<string, Coordinate[]> ControlVolumes => _controlVolumes;
	private static readonly ObservableDictionary<string, Coordinate[]> _controlVolumes = [];

	static ScopeData() =>
		_controlVolumes.Modified += (_, _) => Invalidated?.Invoke();
}
