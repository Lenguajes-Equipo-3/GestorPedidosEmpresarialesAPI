using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class ProductoVarianteData
    {
        private readonly string connectionString;

        public ProductoVarianteData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<VarianteProducto> GetAll()
        {
            var lista = new List<VarianteProducto>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"
        SELECT vp.id_variante, vp.id_producto_base, vp.talla, vp.descripcion, 
               vp.precio, vp.cantidad_existencias, vp.punto_reorden, vp.eliminado,
               pb.id_producto_base, pb.nombre_producto, pb.id_categoria, pb.eliminado AS producto_base_eliminado,
               c.id_categoria, c.descripcion, c.eliminado AS categoria_eliminado
        FROM VarianteProducto vp
        INNER JOIN ProductoBase pb ON vp.id_producto_base = pb.id_producto_base
        INNER JOIN Categoria c ON pb.id_categoria = c.id_categoria
        WHERE vp.eliminado = 0";

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
                    reader.GetBoolean(reader.GetOrdinal("producto_base_eliminado"))
                );

                var varianteProducto = new VarianteProducto(
                    reader.GetInt32(reader.GetOrdinal("id_variante")),
                    productoBase,
                    reader.GetString(reader.GetOrdinal("talla")),
                    reader.GetString(reader.GetOrdinal("descripcion")),
                    reader.GetDecimal(reader.GetOrdinal("precio")),
                    reader.GetDecimal(reader.GetOrdinal("cantidad_existencias")),
                    reader.GetInt32(reader.GetOrdinal("punto_reorden")),
                    reader.GetBoolean(reader.GetOrdinal("eliminado"))
                );

                lista.Add(varianteProducto);
            }

            return lista;
        }

        public VarianteProducto? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
        SELECT vp.id_variante, vp.id_producto_base, vp.talla, vp.descripcion, 
               vp.precio, vp.cantidad_existencias, vp.punto_reorden, vp.eliminado,
               pb.id_producto_base, pb.nombre_producto, pb.id_categoria, pb.eliminado AS producto_base_eliminado,
               c.id_categoria, c.descripcion, c.eliminado AS categoria_eliminado
        FROM VarianteProducto vp
        INNER JOIN ProductoBase pb ON vp.id_producto_base = pb.id_producto_base
        INNER JOIN Categoria c ON pb.id_categoria = c.id_categoria
        WHERE vp.id_variante = @id AND vp.eliminado = 0";

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

                var productoBase = new ProductoBase(
                    reader.GetInt32(reader.GetOrdinal("id_producto_base")),
                    reader.GetString(reader.GetOrdinal("nombre_producto")),
                    categoria,
                    reader.GetBoolean(reader.GetOrdinal("producto_base_eliminado"))
                );

                return new VarianteProducto(
                    reader.GetInt32(reader.GetOrdinal("id_variante")),
                    productoBase,
                    reader.GetString(reader.GetOrdinal("talla")),
                    reader.GetString(reader.GetOrdinal("descripcion")),
                    reader.GetDecimal(reader.GetOrdinal("precio")),
                    reader.GetDecimal(reader.GetOrdinal("cantidad_existencias")),
                    reader.GetInt32(reader.GetOrdinal("punto_reorden")),
                    reader.GetBoolean(reader.GetOrdinal("eliminado"))
                );
            }

            return null;
        }

        public void Insert(VarianteProducto varianteProducto)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                INSERT INTO VarianteProducto (id_producto_base, talla, descripcion, precio, cantidad_existencias, punto_reorden, eliminado)
                VALUES (@idProductoBase, @talla, @descripcion, @precio, @cantidadExistencias, @puntoReorden, 0)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@idProductoBase", varianteProducto.ProductoBase.IdProductoBase);
            command.Parameters.AddWithValue("@talla", varianteProducto.Talla);
            command.Parameters.AddWithValue("@descripcion", varianteProducto.Descripcion);
            command.Parameters.AddWithValue("@precio", varianteProducto.Precio);
            command.Parameters.AddWithValue("@cantidadExistencias", varianteProducto.CantidadExistencias);
            command.Parameters.AddWithValue("@puntoReorden", varianteProducto.PuntoReorden);

            command.ExecuteNonQuery();
        }

        public void Update(VarianteProducto varianteProducto)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                UPDATE VarianteProducto
                SET id_producto_base = @idProductoBase, talla = @talla, descripcion = @descripcion, 
                    precio = @precio, cantidad_existencias = @cantidadExistencias, punto_reorden = @puntoReorden
                WHERE id_variante = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", varianteProducto.IdVariante);
            command.Parameters.AddWithValue("@idProductoBase", varianteProducto.ProductoBase.IdProductoBase);
            command.Parameters.AddWithValue("@talla", varianteProducto.Talla);
            command.Parameters.AddWithValue("@descripcion", varianteProducto.Descripcion);
            command.Parameters.AddWithValue("@precio", varianteProducto.Precio);
            command.Parameters.AddWithValue("@cantidadExistencias", varianteProducto.CantidadExistencias);
            command.Parameters.AddWithValue("@puntoReorden", varianteProducto.PuntoReorden);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "UPDATE VarianteProducto SET eliminado = 1 WHERE id_variante = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}