using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class RolData
    {
        private readonly string _connectionString;

        public RolData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Rol> GetAll()
        {
            var roles = new List<Rol>();
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id_rol, nombre_rol, eliminado FROM Rol WHERE eliminado = 0";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(MapToRol(reader));
                    }
                }
            }
            return roles;
        }

        public Rol GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id_rol, nombre_rol, eliminado FROM Rol WHERE id_rol = @id AND eliminado = 0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToRol(reader);
                    }
                }
            }
            return null;
        }

        public Rol Create(Rol rol)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Rol (nombre_rol, eliminado) VALUES (@nombre_rol, 0); SELECT SCOPE_IDENTITY();";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre_rol", rol.NombreRol);
                connection.Open();
                var id = command.ExecuteScalar();
                rol.IdRol = System.Convert.ToInt32(id);
            }
            return rol;
        }

        public void Update(Rol rol)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Rol SET nombre_rol = @nombre_rol WHERE id_rol = @id_rol";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id_rol", rol.IdRol);
                command.Parameters.AddWithValue("@nombre_rol", rol.NombreRol);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Rol SET eliminado = 1 WHERE id_rol = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private Rol MapToRol(SqlDataReader reader)
        {
            return new Rol
            {
                IdRol = (int)reader["id_rol"],
                NombreRol = (string)reader["nombre_rol"],
                Eliminado = (bool)reader["eliminado"]
            };
        }
    }
}