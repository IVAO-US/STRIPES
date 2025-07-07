namespace STRIPES.Pages;

public sealed partial class MainPage : Page
{
	public MainPage() => InitializeComponent();

	private void Page_Loaded(object sender, RoutedEventArgs e) =>
		XamlRootProvider.Initialize(XamlRoot!);
}

public partial record MainModel(IAuthenticationService AuthenticationService)
{
	public void Authenticate()
	{
		Window.Current!.DispatcherQueue.TryEnqueue(async () => {
			if (!await AuthenticationService.LoginAsync())
				return;
		});
	}
}
