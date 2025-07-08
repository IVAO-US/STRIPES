using STRIPES.Commands;

using IOmnibarCommand = STRIPES.Extensibility.IOmnibarCommand;

namespace STRIPES.Services;

internal sealed class OmnibarService(CommandContainer _commands)
{
	/// <summary>
	/// Called when an omnibar command is submitted.
	/// </summary>
	public async Task QuerySubmittedAsync(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
	{
		var (target, targetId, input) = SplitCommand(args.QueryText);
		sender.Text = "";

		await Task.WhenAll(
			_commands.Get(target)
				.Select(ac => (Match: ac.GetCommandRegex().Match(input), Command: ac))
				.Where(res => res.Match.Success)
				.Select(res => res.Command.ProcessCommandAsync(targetId, res.Match))
		);
	}

	/// <summary>
	/// Called when an omnibar suggestion is chosen.
	/// </summary>
	public static void SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args) =>
		sender.Text = args.SelectedItem.ToString();

	/// <summary>
	/// Called when the text in the omnibar is changed.
	/// </summary>
	public void TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
	{
		if (args.Reason is not AutoSuggestionBoxTextChangeReason.UserInput)
			return;

		var (target, prefix, command) = SplitCommand(sender.Text);
		sender.ItemsSource = GetSuggestions(target, command).Take(10).Select(s => prefix + ' ' + s);
	}

	/// <summary>Splits the <paramref name="input"/> into a target and a command.</summary>
	/// <param name="input">The full string from the omnibar.</param>
	private static (IOmnibarCommand.CommandTarget Target, string TargetId, string Command) SplitCommand(string input)
	{
		input = input.Trim();
		int splitIdx = input.IndexOf(' ');

		if (splitIdx < 0)
			splitIdx = input.Length;

		string targetId = input[..splitIdx];
		IOmnibarCommand.CommandTarget target =
			splitIdx is 3 && targetId.All(char.IsDigit)
			? IOmnibarCommand.CommandTarget.Aircraft
			: IOmnibarCommand.CommandTarget.Window;

		return (target, targetId, input[splitIdx..].TrimStart());
	}

	private IEnumerable<string> GetSuggestions(IOmnibarCommand.CommandTarget target, string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			yield break;

		foreach (IOmnibarCommand command in _commands.Get(target))
			foreach (string suggestion in command.GetSuggestions(input))
				yield return suggestion;
	}
}
