namespace Api.Service.Users;

public static class ApiEndPoints
{
    public const string HealthCheckUsersReady = "api/v{v:apiVersion}/users/health/ready";
    public const string HealthCheckUsersLive = "api/v{v:apiVersion}/users/health/live";
    public const string RootUsers = "api/v{v:apiVersion}/users";
    public const string AuthUsers = "api/v{v:apiVersion}/authenticate";
    public const string UserCreateRecs = "create";
    public const string UserGetAllRecs = "getall";
    public const string UserGetOneRecs = "getone";
    public const string UserDeleteRecs = "delete";
    public const string UserUpdateRecs = "update";
    public const string CheckUsersAuth = "check";
}

public static class ApiMessages
{
    public const string PasswordIsRequired = "El campo 'Password' es requerido";
    public const string UserNameIsRequired = "El campo 'UserName' es requerido";
    public const string UserNameMinSize = "La longitud mímina para el 'UserName' es de 8";
    public const string UserNameMaxSize = "La longitud máxima para el 'UserName' es de 16";
    public const string PasswordMinSize = "La longitud mímina para el 'Password' es de 8";
    public const string PasswordMaxSize = "La longitud máxima para el 'Password' es de 16";
}

public static class ApiVersions
{
    public const double UsersApiV1 = 1.0;
    public const double AuthUserV1 = 1.0;
}