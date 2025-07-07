using Microsoft.Extensions.Configuration;

namespace STRIPES.Services;

internal class SettingsService : ISettings
{
	public static SettingsService Instance { get; private set; } = new();

	public SettingsService(IConfiguration? config = null)
	{
		if (config is null)
			return;

		Queue<IConfigurationSection> frontier = new(config.GetChildren());

		while (frontier.TryDequeue(out var section))
			if (section.Value is null)
				foreach (var subsection in section.GetChildren())
					frontier.Enqueue(subsection);
			else
				Set(section.Path, section.Value);

		Instance ??= this;
	}

	private readonly ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;

	public IReadOnlyCollection<string> Keys => [.. _settings.Values.Keys];

	public void Clear() => _settings.Values.Clear();
	public string? Get(string key) => _settings.Values.TryGetValue(key, out var v) && v is string s ? s : null;
	public void Remove(string key) => _settings.Values.Remove(key);
	public void Set(string key, string? value) => _settings.Values[key] = value;
}
