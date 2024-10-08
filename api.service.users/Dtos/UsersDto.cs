namespace Api.Service.Users.Dtos;

using System.ComponentModel.DataAnnotations;
using Jubatus.WebApi.Extensions.Models;

/// <summary>
/// 
/// </summary>
public record AuthUserDto: ICypherModel
{
    [Required]
    public string AliasName { get; set; } = string.Empty;
    [Required]
    public string UserPass { get; set; } = string.Empty;
}

/// <summary>
/// 
/// </summary>
public record UsersDto: IEntity
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? AliasName { get; init; }
    public string? UserPass { get; init; }
    public bool IsActive { get; init; }
}

/// <summary>
/// 
/// </summary>
public record NewUsersDto: ICypherModel
{
    [Required( ErrorMessage = ApiMessages.UserNameIsRequired )]
    [MinLength( 8, ErrorMessage = ApiMessages.UserNameMinSize )]
    [MaxLength( 16, ErrorMessage = ApiMessages.UserNameMaxSize )]
    public string AliasName { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Required( ErrorMessage = ApiMessages.PasswordIsRequired )]
    [MinLength( 8, ErrorMessage = ApiMessages.PasswordMinSize )]
    [MaxLength( 16, ErrorMessage = ApiMessages.PasswordMaxSize )]
    public string UserPass { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}

/// <summary>
/// 
/// </summary>
public record UpdUsersDto: ICypherModel
{
    [Required( ErrorMessage = ApiMessages.UserNameIsRequired )]
    [MinLength( 8, ErrorMessage = ApiMessages.UserNameMinSize )]
    [MaxLength( 16, ErrorMessage = ApiMessages.UserNameMaxSize )]
    public string AliasName { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Required( ErrorMessage = ApiMessages.PasswordIsRequired )]
    [MinLength( 8, ErrorMessage = ApiMessages.PasswordMinSize )]
    [MaxLength( 16, ErrorMessage = ApiMessages.PasswordMaxSize )]
    public string UserPass { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
};
