namespace Api.Gateway.Users;

/// <summary>
/// 
/// </summary>
public static class ApiEndPoints
{
    public const string HealthCheckGatewayLive = "api/gateway/health/live";
}

/// <summary>
/// 
/// </summary>
public static class ApiConsts
{
    public const string SecurityDefinitionName = "Bearer";
    public const string RateLimiterPolicyName = "fixed";
    public const string ReverseProxy = "ReverseProxy";
    public const string JwtSettings = "JwtSettings";
    public const string JwtKey = "JwtKey";
    public const string JwtIssuer = "JwtIssuer";
    public const string JwtAudience = "JwtAudience";
    public const string ValidateIssuer = "ValidateIssuer";
    public const string ValidateAudience = "ValidateAudience";
    public const string ValidateLifetime = "ValidateLifetime";
    public const string ValidateIssuerSigningKey = "ValidateIssuerSigningKey";
    public const string AuthUser = "AuthUser";
    public const string AuthPass = "AuthPass";
}