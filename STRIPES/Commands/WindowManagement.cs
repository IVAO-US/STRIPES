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
		IOmnibarCommand.PrefixSuggestions(input, "SPAWN");

	int _spawnedWindows = 0;

	public Task ProcessCommandAsync(string target, Match input)
	{
		// Create and launch a window.
		ScopeCanvas childScope = new() {
			Tag = $".{Interlocked.Increment(ref _spawnedWindows)}"
		};

		Window window = new() {
			Content = childScope,
			Title = $"STRIPES - {childScope.Tag}"
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

partial record ExitWindow() : IOmnibarCommand
{
	public string Name => "Exit a scope window.";

	public IOmnibarCommand.CommandTarget Target => IOmnibarCommand.CommandTarget.Window;

	[GeneratedRegex(@"^EXIT$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase)]
	public partial Regex GetCommandRegex();

	public IEnumerable<string> GetSuggestions(string input) =>
		IOmnibarCommand.PrefixSuggestions(input, "EXIT");

	public Task ProcessCommandAsync(string target, Match input)
	{
		if (target is "." or ".0")
			ApplicationHelper.Windows.First(w => w.Content is not null).Close();
		else if (ApplicationHelper.Windows.FirstOrDefault(w =>
					w.Content is ScopeCanvas cnv &&
					cnv.Tag is string tag &&
					tag.Equals(target, StringComparison.InvariantCultureIgnoreCase)) is Window victim)
			victim.Close();

		return Task.CompletedTask;
	}
}
