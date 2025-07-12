using System.Collections.Immutable;

namespace STRIPES.Extensibility;

public record OsmFeature(string Layer, (int Zoom, int X, int Y) Tile, Dictionary<string, object> Tags, ImmutableArray<OsmGeoCommand> Geometry)
{
	public object? this[string tagKey] => Tags.TryGetValue(tagKey, out var val) ? val : null;
}

public abstract record OsmGeoCommand()
{
	public sealed record MoveTo(int X, int Y) : OsmGeoCommand();
	public sealed record LineTo(int X, int Y) : OsmGeoCommand();
	public sealed record ClosePath() : OsmGeoCommand();
}
