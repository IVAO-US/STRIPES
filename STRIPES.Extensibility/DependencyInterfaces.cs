namespace STRIPES.Extensibility;

public interface ITooltipNotifier
{
	/// <summary>
	/// Pops a tooltip with the provided <paramref name="message"/> on the screen.
	/// </summary>
	public void Notify(string message);
}
