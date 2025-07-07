namespace STRIPES.Pages;

public sealed partial class Shell : UserControl, IContentControlProvider
{
	public Shell() => InitializeComponent();

	public ContentControl ContentControl => Splash;
}

public record ShellModel(INavigator Navigator);
