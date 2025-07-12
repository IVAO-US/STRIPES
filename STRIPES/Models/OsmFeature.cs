using STRIPES.Extensibility;
using STRIPES.Services.Protocols;

namespace STRIPES.Models;

internal static class OsmFeatureExtensions
{
	extension(Tile.Types.Layer layer)
	{
#pragma warning disable IDE0079 // Supposedly the below is unnecessary. Roslyn begs to differ.
#pragma warning disable CA1822 // The analysers don't really understand static-ness with extensions in this format yet…
		public IEnumerable<OsmFeature> GetFeatures(int tileZoom, int tileX, int tileY)
		{
			string[] keys = [.. layer.Keys];
			object[] values = [..layer.Values.Select(v =>
						v.HasStringValue ? (object)v.StringValue
						: v.HasBoolValue ? v.BoolValue
						: v.HasIntValue ? v.IntValue
						: ""
					)];

			foreach (var feature in layer.Features)
				yield return GenerateFeature(layer.Name, (tileZoom, tileX, tileY), feature, keys, values);
		}
	}
#pragma warning restore

	public static IEnumerable<T[]> SplitNull<T>(this IEnumerable<Nullable<T>> seq) where T : struct
	{
		List<T> values = [];
		foreach (T? val in seq)
			if (val is null)
			{
				yield return [.. values];
				values.Clear();
			}
			else
				values.Add(val.Value);

		if (values.Count > 0)
			yield return [.. values];
	}

	private static OsmFeature GenerateFeature(string layer, (int Zoom, int X, int Y) tile, Tile.Types.Feature feature, string[] keys, object[] values)
	{
		Dictionary<string, object> tags = [];
		List<OsmGeoCommand> geometry = [];

		if (feature.Tags.Count % 2 != 0)
			throw new ArgumentException("OSM features must have an even number of tag indices.", nameof(feature));

		for (int idx = 0; idx < feature.Tags.Count - 1; idx += 2)
			tags[keys[feature.Tags[idx]]] = values[feature.Tags[idx + 1]];

		for (int idx = 0; idx < feature.Geometry.Count; ++idx)
		{
			uint current() => feature.Geometry[idx];
			uint next() => feature.Geometry[++idx];
			int param(uint raw) => (int)(raw >> 1) ^ -(int)(raw & 1);

			uint id = current() & 0x07,
				 count = current() >> 3;

			while (count-- > 0)
				geometry.Add(id switch {
					1 => new OsmGeoCommand.MoveTo(param(next()), param(next())),
					2 => new OsmGeoCommand.LineTo(param(next()), param(next())),
					7 => new OsmGeoCommand.ClosePath(),
					_ => throw new NotImplementedException()
				});
		}

		return new(layer, tile, tags, [.. geometry]);
	}
}
