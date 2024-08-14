namespace Api.Service.Users.Settings;

public record JwtSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string? JwtKey { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? JwtIssuer { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? JwtAudience { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidateIssuer { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidateAudience { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidateLifetime { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidateIssuerSigningKey { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? AuthUser { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? AuthPass { get; init; }
}