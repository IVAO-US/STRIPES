using Microsoft.UI;
using Microsoft.UI.Xaml.Data;

namespace STRIPES.Pages;

public sealed partial class MainPage : Page
{
	public MainPage() => InitializeComponent();

	private void Page_Loaded(object sender, RoutedEventArgs e) => XamlRootProvider.Initialize(XamlRoot!);
}

internal partial record MainModel(IAuthenticationService AuthenticationService, IvaoApiService IvaoApi, INavigator Navigator, IvanConnectionService Ivan)
{
	public IState<string> SelectedPosition => State<string>.Value(this, () => "");
	public IState<string> PositionAuthorisationMessage => State<string>.Value(this, () => "No checks performed");
	public IState<bool> PositionAuthorised => State<bool>.Value(this, () => false);
}

public class SuccessFailBrushConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language) =>
		value is bool b
		? new SolidColorBrush(b ? Colors.Green : Colors.Red)
		: throw new ArgumentException("Invalid value. Expected a boolean.", nameof(value));

	public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
