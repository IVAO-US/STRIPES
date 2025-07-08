namespace STRIPES.Pages;

public sealed partial class Shell : UserControl, IContentControlProvider
{
	public Shell() => InitializeComponent();

	public ContentControl ContentControl => Splash;

	public Page LoadedChild => (Page)((Frame)((FrameView)((ExtendedSplashScreen)((Border)this.Content).Child).Content).Content).Content;
}

public record ShellModel(INavigator Navigator);
