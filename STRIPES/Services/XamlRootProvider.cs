namespace STRIPES.Services;

internal static class XamlRootProvider
{
	static XamlRoot? _root;

	public static void Initialize(XamlRoot root) => _root = root;

	public static XamlRoot XamlRoot => _root ?? throw new InvalidOperationException("The provider must be initialised before being used.");
}
