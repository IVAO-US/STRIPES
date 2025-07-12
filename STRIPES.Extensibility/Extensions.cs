namespace STRIPES.Extensibility;

public static class Extensions
{
	/// <summary>
	/// Truncates a <see cref="decimal"/> to a fixed number of <paramref name="digits"/> after the decimal point.
	/// </summary>
	/// <param name="digits">The number of digits after the decimal.</param>
	/// <returns></returns>
	public static decimal Truncate(this decimal number, int digits)
	{
		int multiplier = (int)Math.Pow(10, digits);

		return decimal.Truncate(number * multiplier) / multiplier;
	}
}
