using Destructurama.Attributed;
using Jubatus.Common.Settings;

namespace Api.Service.Users.Settings;

public record MongoDbSettings : IMongoSettings
{
    /// <summary>
    /// Ubicación de la instancia de MongoDB
    /// </summary>
    public string? Host { get; init; }

    /// <summary>
    /// Puerto de la instancia de MongoDB
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Usuario de conexión a la instancia de MongoDB
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Clave de acceso del usuario de conexión a la instancia de MongoDB
    /// </summary>
    [NotLogged]
    public string? UserPass { get; init; }

    /// <summary>
    /// Nombre del Servicio que se conecta a la instancia de MongoDB (Nombre que se tomará para nombrar la BD)
    /// </summary>
    public string? ServiceName { get; init; }

    /// <summary>
    /// Cadena de configuración para la conxión a la instancia de MongoDB
    /// </summary>
    [NotLogged]
    public string ConnectionString => $"mongodb://{UserName}:{UserPass}@{Host}:{Port}";
}