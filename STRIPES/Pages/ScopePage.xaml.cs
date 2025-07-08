using System.Runtime.CompilerServices;

namespace STRIPES.Pages;

internal partial class ScopePage : Page
{
	public ScopeModel Model => ((ScopeViewModel)DataContext).Model;
	public ScopeCanvas Canvas => ScpBackground;

	public ScopePage()
	{
		InitializeComponent();
		ScopeData.Invalidated += ScpBackground.Invalidate;
		AsbOmnibar.Focus(FocusState.Keyboard);
	}

	private void Page_GotFocus(object sender, RoutedEventArgs e) => AsbOmnibar.Focus(FocusState.Keyboard);

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		// Focus the omnibar.
		Page_GotFocus(sender, e);

		// Set the popup fade triggers.
		Task.Run(async () => {
			while (DataContext is null)
				await Task.Delay(100);

			Model.TooltipUpdated += Model_TooltipUpdated;
		});
	}

	private void Model_TooltipUpdated()
	{
		DispatcherQueue.TryEnqueue(() => {
			TtpPopup.IsOpen = true;
			StbFadeTooltip.Begin();
			StbFadeTooltip.Completed += (_, _) => TtpPopup.IsOpen = false;
		});
	}
}

internal partial record ScopeModel(OmnibarService Omnibar)
{
	public event Action? TooltipUpdated;
	public IFeed<string> Tooltip => Feed.AsyncEnumerable(Tooltips);

	private async IAsyncEnumerable<string> Tooltips([EnumeratorCancellation] CancellationToken tkn)
	{
		foreach (string tooltip in Omnibar.Tooltips(tkn))
		{
			TooltipUpdated?.Invoke();
			yield return tooltip;
			await Task.Delay(TimeSpan.FromSeconds(0.5), tkn);
		}
	}
}
