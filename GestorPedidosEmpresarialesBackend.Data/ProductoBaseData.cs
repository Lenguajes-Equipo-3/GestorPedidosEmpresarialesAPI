using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class ProductoBaseData
    {
        private readonly string connectionString;

        public ProductoBaseData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProductoBase> GetAll()
        {
            var lista = new List<ProductoBase>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"
                SELECT pb.id_producto_base, pb.nombre_producto, pb.id_categoria, pb.eliminado, 
                       c.id_categoria, c.descripcion, c.eliminado AS categoria_eliminado
                FROM ProductoBase pb
                INNER JOIN Categoria c ON pb.id_categoria = c.id_categoria
                WHERE pb.eliminado = 0";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var categoria = new Categoria(
                    reader.GetInt32(reader.GetOrdinal("id_categoria")),
                    reader.GetString(reader.GetOrdinal("descripcion")),
                    reader.GetBoolean(reader.GetOrdinal("categoria_eliminado"))
                );

                var productoBase = new ProductoBase(
                    reader.GetInt32(reader.GetOrdinal("id_producto_base")),
                    reader.GetString(reader.GetOrdinal("nombre_producto")),
                    categoria,
                    reader.GetBoolean(reader.GetOrdinal("eliminado"))
                );

                lista.Add(productoBase);
            }

            return lista;
        }

        public ProductoBase? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                SELECT pb.id_producto_base, pb.nombre_producto, pb.id_categoria, pb.eliminado, 
                       c.id_categoria, c.descripcion, c.eliminado AS categoria_eliminado
                FROM ProductoBase pb
                INNER JOIN Categoria c ON pb.id_categoria = c.id_categoria
                WHERE pb.id_producto_base = @id AND pb.eliminado = 0";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var categoria = new Categoria(
                    reader.GetInt32(reader.GetOrdinal("id_categoria")),
                    reader.GetString(reader.GetOrdinal("descripcion")),
                    reader.GetBoolean(reader.GetOrdinal("categoria_eliminado"))
                );

                return new ProductoBase(
                    reader.GetInt32(reader.GetOrdinal("id_producto_base")),
                    reader.GetString(reader.GetOrdinal("nombre_producto")),
                    categoria,
                    reader.GetBoolean(reader.GetOrdinal("eliminado"))
                );
            }

            return null;
        }

        public void Insert(ProductoBase productoBase)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                INSERT INTO ProductoBase (nombre_producto, id_categoria, eliminado)
                VALUES (@nombre, @idCategoria, 0)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", productoBase.NombreProducto);
            command.Parameters.AddWithValue("@idCategoria", productoBase.Categoria.IdCategoria);

            command.ExecuteNonQuery();
        }

        public void Update(ProductoBase productoBase)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                UPDATE ProductoBase
                SET nombre_producto = @nombre, id_categoria = @idCategoria
                WHERE id_producto_base = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", productoBase.IdProductoBase);
            command.Parameters.AddWithValue("@nombre", productoBase.NombreProducto);
            command.Parameters.AddWithValue("@idCategoria", productoBase.Categoria.IdCategoria);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "UPDATE ProductoBase SET eliminado = 1 WHERE id_producto_base = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}