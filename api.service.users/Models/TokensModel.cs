namespace Api.Service.Users.Models;

public record TokensModel
{
    /// <summary>
    /// 
    /// </summary>
    public string Token { get; set; } = String.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string RefreshToken { get; set; } = String.Empty;
}