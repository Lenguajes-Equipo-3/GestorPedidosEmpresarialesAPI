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