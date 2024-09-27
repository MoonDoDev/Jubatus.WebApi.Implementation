namespace Api.Service.Users.Models;

using Jubatus.WebApi.Extensions.Models;

public record UsersEntity: IEntity
{
    public Guid Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? AliasName { get; init; }

    public string? UserPass { get; init; }

    public bool IsActive { get; init; }
}