using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ClinicaMedica.Models.Clases;
using ClinicaMedica.Utilities;

namespace ClinicaMedica.Data
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Database=ClinicaMedica;Integrated Security=True;Connection Timeout=5;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Usuario ValidarUsuario(string nombreUsuario, string password)
        {
            // Si no hay conexión a la BD, usar modo prueba inmediatamente
            if (!TestConnection())
            {
                return ValidarUsuarioTemporal(nombreUsuario, password);
            }

            Usuario usuario = null;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT u.Id, u.NombreUsuario, u.ContraseñaHash, u.RolId, r.Nombre as RolNombre, 
                               u.Activo, u.FechaCreacion 
                        FROM Usuarios u 
                        INNER JOIN Roles r ON u.RolId = r.Id 
                        WHERE u.NombreUsuario = @NombreUsuario AND u.Activo = 1";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hash = reader["ContraseñaHash"].ToString();

                                if (SecurityHelper.VerifyPassword(password, hash))
                                {
                                    usuario = new Usuario
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        NombreUsuario = reader["NombreUsuario"].ToString(),
                                        ContraseñaHash = hash,
                                        RolId = Convert.ToInt32(reader["RolId"]),
                                        RolNombre = reader["RolNombre"].ToString(),
                                        Activo = Convert.ToBoolean(reader["Activo"]),
                                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                // En caso de error, usar modo temporal
                return ValidarUsuarioTemporal(nombreUsuario, password);
            }

            return usuario;
        }

        public Usuario ValidarUsuarioTemporal(string nombreUsuario, string password)
        {
            // Usuarios temporales para testing
            if (nombreUsuario == "admin" && password == "admin123")
            {
                return new Usuario
                {
                    Id = 1,
                    NombreUsuario = "admin",
                    ContraseñaHash = "",
                    RolId = 1,
                    RolNombre = "Administrador",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };
            }
            else if (nombreUsuario == "medico" && password == "medico123")
            {
                return new Usuario
                {
                    Id = 2,
                    NombreUsuario = "medico",
                    ContraseñaHash = "",
                    RolId = 2,
                    RolNombre = "Médico",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };
            }
            else if (nombreUsuario == "recepcion" && password == "recepcion123")
            {
                return new Usuario
                {
                    Id = 3,
                    NombreUsuario = "recepcion",
                    ContraseñaHash = "",
                    RolId = 3,
                    RolNombre = "Recepcionista",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };
            }

            return null;
        }
    }
}