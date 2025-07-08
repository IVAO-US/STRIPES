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

		if ((ApplicationHelper.Windows.GetCanvasByWindow(target) ?? ApplicationHelper.Windows.GetCanvasByWindow(".")) is not ScopeCanvas canvas)
			return Task.CompletedTask;

		// Copy the size.
		canvas.CopyBounds(childScope);

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
		if (ApplicationHelper.Windows.GetWindow(target) is Window victim)
			victim.Close();

		return Task.CompletedTask;
	}
}

public static class WindowExtensions
{
	public static Window? GetWindow(this IReadOnlyList<Window> windows, string tag)
	{
		if (tag is "." or ".0")
			return windows.First(w => w.Content is not null);
		else if (windows.FirstOrDefault(w =>
					w.Content is ScopeCanvas cnv &&
					cnv.Tag is string cnvTag &&
					cnvTag.Equals(tag, StringComparison.InvariantCultureIgnoreCase)) is Window win)
			return win;
		else
			return null;
	}

	public static IEnumerable<ScopeCanvas> GetAllCanvases(this IReadOnlyList<Window> windows)
	{
		foreach (Window window in windows)
			if (window.Content is Shell shell && shell.Content is ScopePage page)
				yield return page.Canvas;
			else if (window.Content is ScopeCanvas canvas)
				yield return canvas;
	}

	public static ScopeCanvas? GetCanvasByWindow(this IReadOnlyList<Window> windows, string tag)
	{
		if (GetWindow(windows, tag) is not Window window)
			return null;

		if (window.Content is Shell shell && shell.LoadedChild is ScopePage page)
			return page.Canvas;
		else if (window.Content is ScopeCanvas canvas)
			return canvas;
		else
			return null;
	}
}
