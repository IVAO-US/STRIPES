using System.Text.RegularExpressions;

namespace STRIPES.Extensibility;

public interface IOmnibarCommand
{
	/// <summary>
	/// A human-readable name for the <see cref="IOmnibarCommand"/>.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The <see cref="CommandTarget"/> which is handled by this <see cref="IOmnibarCommand"/>.
	/// </summary>
	public CommandTarget Target { get; }

	/// <summary>
	/// Generates a <see cref="Regex"/> which, if matched, will cause an omnibar input to be processed by this <see cref="IOmnibarCommand"/>.
	/// </summary>
	/// <remarks>
	///	NOTE: This <see cref="Regex"/> may or may not be cached for any length of time and, as such, should always return the same result.
	/// </remarks>
	public Regex GetCommandRegex();

	/// <summary>
	/// Produces possible completion options for a given <paramref name="input"/>.
	/// </summary>
	/// <param name="input">The partial omnibar input to suggest completions for.</param>
	/// <remarks>
	///	NOTE: As execution may not always start or be terminated prematurely, side-effects should be avoided and may lead to non-deterministic effects.
	/// </remarks>
	public IEnumerable<string> GetSuggestions(string input);

	/// <summary>
	/// Processes the given <paramref name="input"/> against the given <paramref name="target"/>.
	/// </summary>
	/// <param name="target">The identifier of the <see cref="CommandTarget"/>.</param>
	/// <param name="input">The results of matching the omnibar input against <see cref="GetCommandRegex"/>.</param>
	public Task ProcessCommandAsync(string target, Match input);

	[Flags]
	public enum CommandTarget
	{
		/// <summary>Handles window-targeted commands.</summary>
		Window = 0b01,

		/// <summary>Handles aircraft-targeted commands.</summary>
		Aircraft = 0b10,

		/// <summary>Handles commands with any target.</summary>
		Any = Window | Aircraft
	}
}
