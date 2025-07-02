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

        public void Insert(Departamento departamento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Departamento (nombre_departamento, eliminado) VALUES (@nombre, 0)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", departamento.NombreDepartamento);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Departamento departamento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Departamento SET nombre_departamento = @nombre WHERE id_departamento = @id AND eliminado = 0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", departamento.IdDepartamento);
                command.Parameters.AddWithValue("@nombre", departamento.NombreDepartamento);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Departamento SET eliminado = 1 WHERE id_departamento = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
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