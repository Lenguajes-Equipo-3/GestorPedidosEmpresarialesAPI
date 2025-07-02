using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class OrdenData
    {
        private readonly string _connectionString;

        public OrdenData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 1. Obtener todas las órdenes con datos de cliente y empleado
        public List<Orden> ObtenerOrdenes()
        {
            var lista = new List<Orden>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_ObtenerOrdenesConClienteEmpleado", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Cliente
                        var cliente = new Cliente
                        {
                            IdCliente = 0, // No viene el id_cliente en el SP, agregar si lo retorna
                            NombreCompania = reader["nombre_compania"].ToString(),
                            NombreContacto = reader["nombre_contacto"].ToString(),
                            ApellidoContacto = reader["apellido_contacto"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            // Complete según sea necesario...
                        };

                        // Departamento
                        var departamento = new Departamento
                        {
                            NombreDepartamento = reader["nombre_departamento"].ToString()
                        };

                        // Empleado
                        var empleado = new Empleado
                        {
                            NombreEmpleado = reader["nombre_empleado"].ToString(),
                            ApellidosEmpleado = reader["apellidos_empleado"].ToString(),
                            Puesto = reader["puesto"].ToString(),
                            Departamento = departamento
                        };

                        // Orden
                        var orden = new Orden
                        {
                            IdOrden = Convert.ToInt32(reader["id_orden"]),
                            Cliente = cliente,
                            Empleado = empleado,
                            FechaOrden = Convert.ToDateTime(reader["fecha_orden"]),
                            DireccionViaje = reader["direccion_viaje"].ToString(),
                            CiudadViaje = reader["ciudad_viaje"].ToString(),
                            ProvinciaViaje = reader["provincia_viaje"].ToString(),
                            PaisViaje = reader["pais_viaje"].ToString(),
                            TelefonoViaje = reader["telefono_viaje"].ToString(),
                            Eliminado = false, // Siempre trae no eliminadas
                            DetallesOrden = ObtenerDetallesPorOrden(Convert.ToInt32(reader["id_orden"]))
                        };



                        lista.Add(orden);
                    }
                }
            }

            return lista;
        }

        public List<DetalleOrden> ObtenerDetallesPorOrden(int idOrden)
        {
            var detalles = new List<DetalleOrden>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_ObtenerDetallesPorOrden", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdOrden", idOrden);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Producto Base
                        var productoBase = new ProductoBase(
                            idProductoBase: 0, // No viene el id, agregar si lo retorna
                            nombreProducto: reader["nombre_producto"].ToString(),
                            categoria: null, // No retorna la categoría aquí, solo nombre de producto
                            eliminado: false
                        );

                        // Variante Producto
                        var variante = new VarianteProducto
                        {
                            IdVariante = Convert.ToInt32(reader["id_variante"]),
                            Talla = reader["talla"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            ProductoBase = productoBase
                        };

                        // Detalle Orden
                        var detalle = new DetalleOrden(
                            idDetalleOrden: Convert.ToInt32(reader["id_detalle_orden"]),
                            varianteProducto: variante,
                            cantidad: Convert.ToInt32(reader["cantidad"]),
                            precioLinea: Convert.ToDouble(reader["precio_linea"]),
                            eliminado: false // Ya filtra solo activos
                        );

                        detalles.Add(detalle);
                    }
                }
            }
            return detalles;
        }

        public int InsertarOrdenConDetalles(Orden orden)
        {
            int idOrdenGenerado = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar la orden
                        using (var cmdOrden = new SqlCommand("InsertarOrden", connection, transaction))
                        {
                            cmdOrden.CommandType = CommandType.StoredProcedure;

                            cmdOrden.Parameters.AddWithValue("@id_cliente", orden.Cliente.IdCliente);
                            cmdOrden.Parameters.AddWithValue("@id_empleado", orden.Empleado.IdEmpleado);
                            cmdOrden.Parameters.AddWithValue("@fecha_orden", orden.FechaOrden);
                            cmdOrden.Parameters.AddWithValue("@direccion_viaje", orden.DireccionViaje ?? (object)DBNull.Value);
                            cmdOrden.Parameters.AddWithValue("@ciudad_viaje", orden.CiudadViaje ?? (object)DBNull.Value);
                            cmdOrden.Parameters.AddWithValue("@provincia_viaje", orden.ProvinciaViaje ?? (object)DBNull.Value);
                            cmdOrden.Parameters.AddWithValue("@pais_viaje", orden.PaisViaje ?? (object)DBNull.Value);
                            cmdOrden.Parameters.AddWithValue("@telefono_viaje", orden.TelefonoViaje ?? (object)DBNull.Value);
                            cmdOrden.Parameters.AddWithValue("@eliminado", orden.Eliminado);

                            var outputParam = new SqlParameter("@id_orden_generado", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmdOrden.Parameters.Add(outputParam);

                            cmdOrden.ExecuteNonQuery();
                            idOrdenGenerado = (int)outputParam.Value;
                        }

                        // 2. Insertar los detalles de la orden
                        foreach (var detalle in orden.DetallesOrden)
                        {
                            using (var cmdDetalle = new SqlCommand("InsertarDetalleOrden", connection, transaction))
                            {
                                cmdDetalle.CommandType = CommandType.StoredProcedure;

                                cmdDetalle.Parameters.AddWithValue("@id_orden", idOrdenGenerado);
                                cmdDetalle.Parameters.AddWithValue("@id_variante", detalle.VarianteProducto.IdVariante);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precio_linea", detalle.PrecioLinea);
                                cmdDetalle.Parameters.AddWithValue("@eliminado", detalle.Eliminado);

                                cmdDetalle.ExecuteNonQuery();
                            }
                        }

                        // 3. Confirmar toda la transacción
                        transaction.Commit();
                        return idOrdenGenerado;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; // Puedes capturar la excepción para retornar un mensaje más amigable si lo deseas
                    }
                }
            }
        }




    }
}
