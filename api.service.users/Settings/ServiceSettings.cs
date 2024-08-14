namespace Api.Service.Users.Settings;

public record ServiceSettings
{
    /// <summary>
    /// Nombre del Servicio que se conecta a la instancia de MongoDB (Nombre que se tomará para nombrar la BD)
    /// </summary>
    public string? ServiceName { get; init; }
}