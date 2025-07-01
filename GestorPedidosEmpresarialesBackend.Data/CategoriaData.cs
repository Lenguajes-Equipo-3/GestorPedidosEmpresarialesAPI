using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{

    public class CategoriaData
    {
        private readonly string connectionString;

        public CategoriaData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Categoria> GetAllCategorias()
        {
            var lista = new List<Categoria>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT id_categoria, descripcion, eliminado FROM Categoria WHERE eliminado = 0";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var categoria = new Categoria(
                    idCategoria: reader.GetInt32(reader.GetOrdinal("id_categoria")),
                    descripcion: reader.GetString(reader.GetOrdinal("descripcion")),
                    eliminado: false
                );

                lista.Add(categoria);
            }

            return lista;
        }

        public Categoria? GetCategoriaById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT id_categoria, descripcion, eliminado FROM Categoria WHERE id_categoria = @id AND eliminado = 0";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Categoria(
                    idCategoria: reader.GetInt32(reader.GetOrdinal("id_categoria")),
                    descripcion: reader.GetString(reader.GetOrdinal("descripcion")),
                    eliminado: false
                );
            }

            return null;
        }

        public void InsertCategoria(Categoria categoria)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                INSERT INTO Categoria (descripcion, eliminado)
                VALUES (@descripcion, @eliminado)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
            command.Parameters.AddWithValue("@eliminado", categoria.Eliminado);

            command.ExecuteNonQuery();
        }

        public void DeleteCategoria(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "UPDATE Categoria SET eliminado = 1 WHERE id_categoria = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }

}
