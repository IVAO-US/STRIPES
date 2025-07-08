using System.Diagnostics.CodeAnalysis;

namespace STRIPES.Services;

internal sealed class IvanConnectionService
{
	public string? Callsign { get; private set; }

	[MemberNotNullWhen(true, nameof(Callsign))]
	public bool IsConnected => Callsign is not null;

	/// <summary>
	/// Connects the authed user to the IVAO network (IVAN).
	/// </summary>
	/// <param name="callsign">The position to connect to.</param>
	/// <exception cref="InvalidOperationException">The user is already connected.</exception>
	[MemberNotNull(nameof(Callsign))]
	public Task ConnectAsync(string callsign)
	{
		if (IsConnected)
			throw new InvalidOperationException("Already connected to IVAN.");

		Callsign = callsign;
		return Task.CompletedTask;
	}

	/// <summary>
	/// Disconnects the user from the IVAO network (IVAN).
	/// </summary>
	/// <param name="callsign">The position to connect to.</param>
	public Task DisconnectAsync()
	{
		if (IsConnected)
			Callsign = null;

		return Task.CompletedTask;
	}
}
