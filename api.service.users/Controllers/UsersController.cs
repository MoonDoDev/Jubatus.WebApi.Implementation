namespace Api.Service.Users.Controllers;

using Asp.Versioning;
using Jubatus.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Api.Service.Users.Dtos;
using Api.Service.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

/// <summary>
/// 
/// </summary>
/// <param name="usersRepository"></param>
/// <param name="logger"></param>
/// <param name="configuration"></param>
[Authorize]
[ApiController]
[ApiVersion( ApiVersions.UsersApiV1 )]
[Route( ApiEndPoints.RootUsers )]
[EnableRateLimiting( "fixed" )]
public class UsersController(
    IRepository<UsersEntity> usersRepository,
    ILogger<UsersController> logger,
    IConfiguration configuration ): ControllerBase
{
    private readonly IRepository<UsersEntity> _usersRepository = usersRepository;
    private readonly ILogger<UsersController> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [MapToApiVersion( ApiVersions.UsersApiV1 )]
    [Route( ApiEndPoints.UserGetAllRecs )]
    public async IAsyncEnumerable<UsersDto> GetAllRecordsAsync()
    {
        var records = _usersRepository.GetAllAsync();
        await foreach( var record in records )
        {
            yield return record.AsUsersDto();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [MapToApiVersion( ApiVersions.UsersApiV1 )]
    [Route( ApiEndPoints.UserGetOneRecs )]
    public async Task<IActionResult> GetOneRecordAsync( [FromQuery] Guid id )
    {
        FastLogger.LogDebug( _logger, $"GettingOneRecord with id: {id}", null );

        var result = await _usersRepository.GetAsync( id ).ConfigureAwait( false );
        return result.IsSuccess ? Ok( result.Value.AsUsersDto() ) : NotFound();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [HttpPost]
    [MapToApiVersion( ApiVersions.UsersApiV1 )]
    [Route( ApiEndPoints.UserCreateRecs )]
    public async Task<IActionResult> CreateRecordAsync( [FromBody] NewUsersDto item )
    {
        ArgumentNullException.ThrowIfNull( item );
        FastLogger.LogDebug( _logger, $"CreatingRecord with alias: {item.AliasName}", null );

        if( !ModelState.IsValid )
        {
            return BadRequest( ModelState );
        }

        UsersEntity newUser = new()
        {
            Id = Guid.NewGuid(),
            AliasName = item.AliasName,
            FirstName = item.FirstName,
            LastName = item.LastName,
            UserPass = item.EncryptUserPassword( _configuration ),
            IsActive = item.IsActive
        };

        var result = await _usersRepository.CreateAsync( newUser ).ConfigureAwait( false );
        return result.IsSuccess ? Ok( result.Value.AsUsersDto() ) : BadRequest();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [HttpPut]
    [MapToApiVersion( ApiVersions.UsersApiV1 )]
    [Route( ApiEndPoints.UserUpdateRecs )]
    public async Task<IActionResult> UpdateRecordAsync( [FromQuery] Guid id, [FromBody] UpdUsersDto item )
    {
        ArgumentNullException.ThrowIfNull( item );
        FastLogger.LogDebug( _logger, $"UpdatingRecord with id: {id}", null );

        if( !ModelState.IsValid )
        {
            return BadRequest( ModelState );
        }

        var currentItem = await _usersRepository.GetAsync( id ).ConfigureAwait( false );

        if( currentItem.IsFailed )
        {
            return NotFound();
        }

        var updatedItem = currentItem.Value with
        {
            AliasName = item.AliasName,
            FirstName = item.FirstName,
            LastName = item.LastName,
            UserPass = item.EncryptUserPassword( _configuration ),
            IsActive = item.IsActive
        };

        var result = await _usersRepository.UpdateAsync( updatedItem ).ConfigureAwait( false );
        return result.IsSuccess ? Ok( result.Value.AsUsersDto() ) : NotFound();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [MapToApiVersion( ApiVersions.UsersApiV1 )]
    [Route( ApiEndPoints.UserDeleteRecs )]
    public async Task<IActionResult> DeleteRecordAsync( [FromQuery] Guid id )
    {
        FastLogger.LogDebug( _logger, $"DeletingRecord with id: {id}", null );

        var result = await _usersRepository.RemoveAsync( id ).ConfigureAwait( false );
        return result.IsSuccess ? NoContent() : NotFound();
    }
}