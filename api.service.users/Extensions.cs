using Api.Service.Users.Dtos;
using Api.Service.Users.Models;

namespace Api.Service.Users;

/// <summary>
/// 
/// </summary>
internal static class UsersExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="usersEntity"></param>
    /// <returns></returns>
    public static UsersDto AsUsersDto( this UsersEntity usersEntity )
    {
        ArgumentNullException.ThrowIfNull( usersEntity );

        return new UsersDto()
        {
            Id = usersEntity.Id,
            FirstName = usersEntity.FirstName!,
            LastName = usersEntity.LastName!,
            AliasName = usersEntity.AliasName!,
            UserPass = usersEntity.UserPass!,
            IsActive = usersEntity.IsActive
        };
    }
}
