namespace DotNetBoilerplate.Infrastructure.Cors;

internal sealed class CorsOptions
{
    public const string SectionName = "Cors";

    public string[] AllowedOrigins { get; set; }
}