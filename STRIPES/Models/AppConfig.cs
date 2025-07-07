namespace STRIPES.Models;

public record AppConfig
{
	public string? Environment { get; init; }
}

public sealed record Oidc()
{
	public string? Authority { get; init; }
	public string? RedirectUri { get; init; }
	public string? Scope { get; init; }
	public string? ClientId { get; init; }
	public string? ClientSecret { get; init; }
}
