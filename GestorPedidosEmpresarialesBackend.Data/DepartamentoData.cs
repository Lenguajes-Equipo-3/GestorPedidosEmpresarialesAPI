using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class DepartamentoData
    {
        private readonly string _connectionString;

        public DepartamentoData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Departamento> GetAll()
        {
            var departamentos = new List<Departamento>();
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id_departamento, nombre_departamento, eliminado FROM Departamento WHERE eliminado = 0";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departamentos.Add(MapToDepartamento(reader));
                    }
                }
            }
            return departamentos;
        }

        public Departamento GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id_departamento, nombre_departamento, eliminado FROM Departamento WHERE id_departamento = @id AND eliminado = 0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToDepartamento(reader);
                    }
                }
            }
            return null;
        }

        private Departamento MapToDepartamento(SqlDataReader reader)
        {
            return new Departamento
            {
                IdDepartamento = (int)reader["id_departamento"],
                NombreDepartamento = (string)reader["nombre_departamento"],
                Eliminado = (bool)reader["eliminado"]
            };
        }
    }
}