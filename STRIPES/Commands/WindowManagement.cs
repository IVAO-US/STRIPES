using STRIPES.Extensibility;

using System.Text.RegularExpressions;

namespace STRIPES.Commands.WindowManagement;

partial record SpawnWindow() : IOmnibarCommand
{
	public string Name => "Spawn new scope windows.";

	public IOmnibarCommand.CommandTarget Target => IOmnibarCommand.CommandTarget.Window;

	[GeneratedRegex(@"^SPAWN$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase)]
	public partial Regex GetCommandRegex();

	public IEnumerable<string> GetSuggestions(string input) =>
		input.Length < "SPAWN".Length && "SPAWN"[..input.Length].Equals(input, StringComparison.InvariantCultureIgnoreCase)
		? ["SPAWN"]
		: [];

	int _spawnedWindows = 0;

	public Task ProcessCommandAsync(string target, Match input)
	{
		// Create and launch a window.
		ScopeCanvas childScope = new();
		Window window = new() {
			Content = childScope,
			Title = $"STRIPES - .{Interlocked.Increment(ref _spawnedWindows)}"
		};

		window.Closed += (_, _) => window.Content = null;
		window.Activate();

		ScopeData.Invalidated += () => {
			try { childScope.Invalidate(); }
			catch { }
		};

		return Task.CompletedTask;
	}
}
