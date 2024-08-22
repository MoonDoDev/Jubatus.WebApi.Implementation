using Destructurama.Attributed;
using Jubatus.WebApi.Extensions.Models;

namespace Api.Service.Users.Models;

public record UsersEntity: IEntity
{
    [NotLogged]
    public Guid Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? AliasName { get; init; }

    [NotLogged]
    public string? Password { get; init; }

    public bool IsActive { get; init; }
}