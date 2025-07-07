namespace STRIPES.Services;

internal sealed class IvanConnectionService
{
	public string? Callsign { get; private set; }
	public bool IsConnected => Callsign is not null;

	/// <summary>
	/// Connects the authed user to the IVAO network (IVAN).
	/// </summary>
	/// <param name="callsign">The position to connect to.</param>
	/// <exception cref="InvalidOperationException">The user is already connected.</exception>
	public async Task ConnectAsync(string callsign)
	{
		if (IsConnected)
			throw new InvalidOperationException("Already connected to IVAN.");

		Callsign = callsign;
		await Task.Delay(TimeSpan.FromSeconds(1.5));
	}
}
