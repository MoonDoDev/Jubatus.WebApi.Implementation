using Destructurama.Attributed;
using Jubatus.Common.Settings;

namespace Api.Gateway.Users.Settings;

public record JwtSettings : IJwtSettings
{
    /// <summary>
    /// 
    /// </summary>
    [NotLogged]
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
    [NotLogged]
    public string? AuthUser { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [NotLogged]
    public string? AuthPass { get; init; }
}