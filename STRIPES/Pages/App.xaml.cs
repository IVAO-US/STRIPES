using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Configuration;

using STRIPES.Commands;
using STRIPES.Extensibility;

using Uno.Resizetizer;

using static Microsoft.Extensions.Configuration.UserSecretsConfigurationExtensions;

namespace STRIPES.Pages;

public partial class App : Application
{
	/// <summary>
	/// Initializes the singleton application object. This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App() => InitializeComponent();

	protected Window? MainWindow { get; private set; }
	internal IHost? Host { get; private set; }

	protected async override void OnLaunched(LaunchActivatedEventArgs args)
	{
		var configApp =
			this.CreateBuilder(args)
				.Configure(host => host
#if DEBUG
					// Pull the DEBUG appconfig
					.UseEnvironment(Environments.Development)
#endif
					.UseConfiguration(configure: builder => builder
						.EmbeddedSource<App>()
						.Section<Oidc>()
						.ConfigureAppConfiguration(appConfig => appConfig.AddUserSecrets<App>())
					)
				)
				.Build();

		Oidc oauthSettings = configApp.Services.GetRequiredService<IOptions<Oidc>>().Value;
		await configApp.StopAsync();
		configApp.Dispose();

		var builder = this.CreateBuilder(args)
			.UseToolkitNavigation()
			.Configure(host => host
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif
				.UseLogging(configure: (context, logBuilder) => {
					// Configure log levels for different categories of logging
					logBuilder
						.SetMinimumLevel(
							context.HostingEnvironment.IsDevelopment() ?
								LogLevel.Information :
								LogLevel.Warning)

						// Default filters for core Uno Platform namespaces
						.CoreLogLevel(LogLevel.Warning);
				}, enableUnoLogging: true)
				.UseConfiguration(configure: configBuilder =>
					configBuilder
						.EmbeddedSource<App>()
						.Section<AppConfig>()
						.Section<Oidc>()
						.ConfigureAppConfiguration(appConfig => appConfig.AddUserSecrets<App>())
				)
				// Register Json serializers (ISerializer and ISerializer)
				.UseSerialization((context, services) => services.AddContentSerializer(context))
				.UseThemeSwitching()
				.UseHttp((context, services) => {
					services.AddKiotaClient<IvaoApiClient>(
						context,
						options: new() { Url = "https://api.ivao.aero/" }
					);
				})
				.UseAuthentication(auth =>
					auth.AddOidc(oidc =>
						oidc
							.ConfigureOidcClientOptions(opts => {
								opts.Policy.Discovery.Authority = "https://api.ivao.aero/";
								opts.Policy.Discovery.DiscoveryDocumentPath = "/.well-known/openid-configuration";
								opts.Policy.Discovery.AdditionalEndpointBaseAddresses.Add("https://sso.ivao.aero/authorize");
								opts.Policy.Discovery.AdditionalEndpointBaseAddresses.Add("https://sso.ivao.aero/logout");
							})
							.Authority(oauthSettings.Authority!)
							.RedirectUri(oauthSettings.RedirectUri!)
							.ClientId(oauthSettings.ClientId!)
							.ClientSecret(oauthSettings.ClientSecret!)
							.Scope(oauthSettings.Scope!)
					)
				)
				.ConfigureServices((context, services) => {
					// TODO: Register your services
					services.AddTransient<IBrowser, OidcBrowser>();
					services.AddSingleton<IvaoApiService>();
					services.AddSingleton(new SettingsService(context.Configuration));
					services.AddSingleton<IvanConnectionService>();
					services.AddSingleton<OmnibarService>();
					services.AddSingleton<ITooltipNotifier>(s => s.GetRequiredService<OmnibarService>());
					services.AddSingleton<CommandContainer>();
				})
				.UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
			);
		MainWindow = builder.Window;

#if DEBUG
		MainWindow.UseStudio();
#endif
		MainWindow.SetWindowIcon();

		Host = await builder.NavigateAsync<Shell>();
		MainWindow.Closed += async (_, _) => {
			await Host.StopAsync();
			Host.Dispose();
			Environment.Exit(0);
		};
	}

	private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
	{
		views.Register(
			new ViewMap(ViewModel: typeof(ShellModel)),
			new ViewMap<MainPage, MainModel>(),
			new ViewMap<ScopePage, ScopeModel>()
		);

		routes.Register(new RouteMap("", View: views.FindByViewModel<ShellModel>(), Nested: [
			new("Main", View: views.FindByViewModel<MainModel>()),
			new("Scope", View: views.FindByViewModel<ScopeModel>(), IsDefault: true)
		]));
	}
}
