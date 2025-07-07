using System.Web;

namespace STRIPES.Pages;

internal partial class AuthDialog : ContentDialog
{
	public string OauthReturnUri { get; private set; } = "";
	public CancellationToken Token => _cts.Token;

	readonly Uri _authUrl;
	readonly CancellationTokenSource _cts = new();

	public AuthDialog(Uri authUrl) : base()
	{
		_authUrl = authUrl;

		async void setup()
		{
			InitializeComponent();
			await RunWebview();
		}

		if (Window.Current?.Dispatcher.HasThreadAccess ?? true)
			setup();
		else
			Window.Current.DispatcherQueue.TryEnqueue(setup);

	}

	async Task RunWebview()
	{
		await WvwMain.EnsureCoreWebView2Async();
		WvwMain.NavigationStarting += WvwMain_NavigationStarting;
		WvwMain.Source = _authUrl;
	}

	private void WvwMain_NavigationStarting(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args)
	{
		if (!Uri.TryCreate(args.Uri, UriKind.Absolute, out Uri? oauthUri) || oauthUri.GetLeftPart(UriPartial.Path).TrimEnd('/') != SettingsService.Instance.Get("Oidc:RedirectUri")?.TrimEnd('/'))
			return;

		OauthReturnUri = args.Uri;

		Window.Current!.DispatcherQueue.TryEnqueue(_cts.Cancel);
	}
}
