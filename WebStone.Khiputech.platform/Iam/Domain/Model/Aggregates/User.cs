using System.Text.Json.Serialization;
using WebStone.Khiputech.Platform.Shared.Domain.Model.Entities;

namespace WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;

public partial class User
{
    // Constructor vacío requerido por Entity Framework Core
    public User() { }

    // Constructor para crear un nuevo usuario (Sin pasarle ID, la base de datos lo genera)
    public User(string username, string passwordHash, string type, string permissions)
    {
        Username = username;
        PasswordHash = passwordHash;
        Type = type;
        Permissions = permissions;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Propiedades obligatorias
    public int Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    [JsonIgnore] // Excelente práctica para no exponer la contraseña en las APIs
    public string PasswordHash { get; private set; } = string.Empty;

    public string Type { get; private set; } = "public";

    public string Permissions { get; private set; } = string.Empty;
    

    public User UpdateUsername(string username)
    {
        Username = username;
        UpdatedAt = DateTime.UtcNow; // Cada vez que edites, actualiza la fecha
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public User UpdateType(string type)
    {
        Type = type;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
    
    public User UpdatePermissions(string permissions)
    {
        Permissions = permissions;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
}