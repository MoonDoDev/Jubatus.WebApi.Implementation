namespace Api.Service.Users.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Api.Service.Users.Dtos;
using Jubatus.WebApi.Extensions;
using Microsoft.AspNetCore.RateLimiting;

[Authorize]
[ApiController]
[ApiVersion( ApiVersions.AuthUserV1 )]
[Route( ApiEndPoints.AuthUsers )]
[EnableRateLimiting( "fixed" )]
public class AuthController(
    ILogger<AuthController> logger,
    IConfiguration configuration ): ControllerBase
{
    #region private data

    private readonly ILogger<AuthController> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    #endregion
    #region http methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authUser"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [MapToApiVersion( ApiVersions.AuthUserV1 )]
    [Route( ApiEndPoints.CheckUsersAuth )]
    public async Task<ActionResult> AuthenticateAsync( [FromBody] AuthUserDto authUser )
    {
        ArgumentNullException.ThrowIfNull( authUser );

        FastLogger.LogDebug( _logger, $"The user: {authUser.AliasName} is trying to authenticate.", null );
        var result = Toolbox.GenerateBearerToken( authUser, _configuration );

        if( result is null )
            return await Task.FromResult( Unauthorized() ).ConfigureAwait( false );

        return await Task.FromResult( Ok( result ) ).ConfigureAwait( false );
    }

    #endregion
}
