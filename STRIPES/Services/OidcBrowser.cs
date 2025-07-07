using IdentityModel.OidcClient.Browser;

namespace STRIPES.Services;

internal class OidcBrowser : IBrowser
{
	public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
	{
		AuthDialog dlg = new(new(options.StartUrl)) {
			XamlRoot = XamlRootProvider.XamlRoot
		};

		SemaphoreSlim sem = new(0);

		Window.Current?.DispatcherQueue.TryEnqueue(
			async () => {
				try
				{
					var runner = dlg.ShowAsync();
					dlg.Token.Register(runner.Cancel);
					await runner;
				}
				catch (TaskCanceledException) { }
				finally
				{
					sem.Release();
				}
			}
		);

		await sem.WaitAsync(cancellationToken);
		dlg.Dispose();
		return new() { ResultType = BrowserResultType.Success, Response = dlg.OauthReturnUri };
	}
}
