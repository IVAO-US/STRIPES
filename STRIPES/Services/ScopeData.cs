using STRIPES.Extensibility;

namespace STRIPES.Services;

internal static class ScopeData
{
	public static event Action? Invalidated;
	public static IDictionary<string, Coordinate[]> ControlVolumes => _controlVolumes;
	private static readonly ObservableDictionary<string, Coordinate[]> _controlVolumes = [];

	private static bool _updating = false;

	/// <summary>Begins a batch update. Suppresses invalidation until <see cref="EndUpdate"/> is called.</summary>
	public static void BeginUpdate() => _updating = true;

	/// <summary>Ends a batch update and triggers <see cref="Invalidated"/>.</summary>
	public static void EndUpdate()
	{
		_updating = false;
		Invalidated?.Invoke();
	}

	public static Quadtree Quadtree
	{
		get => field;
		set
		{
			if (field == value) return;

			field = value;
			if (!_updating)
				Invalidated?.Invoke();
		}
	} = Quadtree.Default;

	static ScopeData() =>
		_controlVolumes.Modified += (_, _) => { if (!_updating) Invalidated?.Invoke(); };
}
