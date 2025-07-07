using Microsoft.UI;
using Microsoft.UI.Xaml.Data;

namespace STRIPES.Pages;

public sealed partial class MainPage : Page
{
	public MainPage() => InitializeComponent();

	private void Page_Loaded(object sender, RoutedEventArgs e) => XamlRootProvider.Initialize(XamlRoot!);
}

public partial record MainModel(IAuthenticationService AuthenticationService, IvaoApiService IvaoApi, INavigator Navigator)
{
	public IState<string> SelectedPosition => State<string>.Value(this, () => "");
	public IState<string> PositionAuthorisationMessage => State<string>.Value(this, () => "No checks performed");
	public IState<bool> PositionAuthorised => State<bool>.Value(this, () => false);

	public async Task GetPositionAuthMessageAsync(CancellationToken tkn)
	{
		if (await SelectedPosition is not string pos)
			return;

		(bool authed, string newVal) = await IvaoApi.FraCheckAsync(pos);
		await PositionAuthorised.Update(c => authed, tkn);
		await PositionAuthorisationMessage.Update(c => newVal, tkn);

		if (authed)
			await Navigator.NavigateViewAsync<ScopePage>(this, cancellation: tkn);
	}
}

public class SuccessFailBrushConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language) =>
		value is bool b
		? new SolidColorBrush(b ? Colors.Green : Colors.Red)
		: throw new ArgumentException("Invalid value. Expected a boolean.", nameof(value));

	public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
