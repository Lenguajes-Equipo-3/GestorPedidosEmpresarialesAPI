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
    public class PagoData
    {
        private readonly string connectionString;

        public PagoData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public List<Pago> GetAll()
        {
            var lista = new List<Pago>();

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
        SELECT 
          p.id_pago, p.cantidad_pago, p.fecha_pago, p.num_tarjetaCredito, p.nom_usuario_tarjeta, p.eliminado AS eliminado,
          o.id_orden, o.fecha_orden, o.direccion_viaje, o.ciudad_viaje, o.provincia_viaje, o.pais_viaje, o.telefono_viaje,
          c.id_cliente, c.nombre_compania, c.nombre_contacto, c.apellido_contacto, c.puesto_contacto, c.direccion, c.ciudad, c.provincia, c.codigo_postal, c.pais, c.telefono,
          e.id_empleado, e.nombre_empleado, e.apellidos_empleado, e.puesto, e.extension, e.telefono_trabajo,
          d.id_departamento, d.nombre_departamento,
          r.id_rol, r.nombre_rol,
          m.id_metodo_pago, m.metodo_pago, m.tarjeta_credito
        FROM Pago p
        INNER JOIN Orden o ON p.id_orden = o.id_orden
        INNER JOIN Cliente c ON o.id_cliente = c.id_cliente
        INNER JOIN Empleado e ON o.id_empleado = e.id_empleado
        INNER JOIN Departamento d ON e.id_departamento = d.id_departamento
        INNER JOIN Rol r ON e.id_rol = r.id_rol
        INNER JOIN MetodoPago m ON p.id_metodopago = m.id_metodo_pago
        WHERE p.eliminado = 0";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var pago = new Pago
                {
                    IdPago = reader.GetInt32(reader.GetOrdinal("id_pago")),
                    CantidadPago = (double)reader.GetDecimal(reader.GetOrdinal("cantidad_pago")),
                    FechaPago = reader.GetDateTime(reader.GetOrdinal("fecha_pago")),
                    NumTarjetaCredito = reader.GetString(reader.GetOrdinal("num_tarjetaCredito")),
                    NomUsuarioTarjeta = reader.GetString(reader.GetOrdinal("nom_usuario_tarjeta")),
                    Eliminado = reader.GetBoolean(reader.GetOrdinal("eliminado")),

                    Orden = new Orden
                    {
                        IdOrden = reader.GetInt32(reader.GetOrdinal("id_orden")),
                        FechaOrden = reader.GetDateTime(reader.GetOrdinal("fecha_orden")),
                        DireccionViaje = reader.GetString(reader.GetOrdinal("direccion_viaje")),
                        CiudadViaje = reader.GetString(reader.GetOrdinal("ciudad_viaje")),
                        ProvinciaViaje = reader.GetString(reader.GetOrdinal("provincia_viaje")),
                        PaisViaje = reader.GetString(reader.GetOrdinal("pais_viaje")),
                        TelefonoViaje = reader.GetString(reader.GetOrdinal("telefono_viaje")),

                        Cliente = new Cliente
                        {
                            IdCliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                            NombreCompania = reader.GetString(reader.GetOrdinal("nombre_compania")),
                            NombreContacto = reader.GetString(reader.GetOrdinal("nombre_contacto")),
                            ApellidoContacto = reader.GetString(reader.GetOrdinal("apellido_contacto")),
                            PuestoContacto = reader.GetString(reader.GetOrdinal("puesto_contacto")),
                            Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                            Ciudad = reader.GetString(reader.GetOrdinal("ciudad")),
                            Provincia = reader.GetString(reader.GetOrdinal("provincia")),
                            CodigoPostal = reader.GetString(reader.GetOrdinal("codigo_postal")),
                            Pais = reader.GetString(reader.GetOrdinal("pais")),
                            Telefono = reader.GetString(reader.GetOrdinal("telefono"))
                        },

                        Empleado = new Empleado
                        {
                            IdEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
                            NombreEmpleado = reader.GetString(reader.GetOrdinal("nombre_empleado")),
                            ApellidosEmpleado = reader.GetString(reader.GetOrdinal("apellidos_empleado")),
                            Puesto = reader.GetString(reader.GetOrdinal("puesto")),
                            Extension = reader.GetString(reader.GetOrdinal("extension")),
                            TelefonoTrabajo = reader.GetString(reader.GetOrdinal("telefono_trabajo")),

                            Departamento = new Departamento
                            {
                                IdDepartamento = reader.GetInt32(reader.GetOrdinal("id_departamento")),
                                NombreDepartamento = reader.GetString(reader.GetOrdinal("nombre_departamento"))
                            },

                            Rol = new Rol
                            {
                                IdRol = reader.GetInt32(reader.GetOrdinal("id_rol")),
                                NombreRol = reader.GetString(reader.GetOrdinal("nombre_rol"))
                            }
                        }
                    },

                    MetodoPago = new MetodoPago
                    {
                        IdMetodoPago = reader.GetInt32(reader.GetOrdinal("id_metodo_pago")),
                        Metodo = reader.GetString(reader.GetOrdinal("metodo_pago")),
                        TarjetaCredito = reader.GetString(reader.GetOrdinal("tarjeta_credito"))
                    }
                };

                lista.Add(pago);
            }

            return lista;
        }


        public void Insert(Pago pago)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
        INSERT INTO Pago 
          (id_orden, cantidad_pago, fecha_pago, num_tarjetaCredito, nom_usuario_tarjeta, id_metodopago, eliminado)
        VALUES
          (@idOrden, @cantidadPago, @fechaPago, @numTarjetaCredito, @nomUsuarioTarjeta, @idMetodoPago, 0);
        SELECT CAST(scope_identity() AS int);
    ";

            using var cmd = new SqlCommand(query, connection);

            // Extraemos solo los IDs necesarios de los objetos relacionados
            cmd.Parameters.AddWithValue("@idOrden", pago.Orden.IdOrden);
            cmd.Parameters.AddWithValue("@cantidadPago", (decimal)pago.CantidadPago);
            cmd.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
            cmd.Parameters.AddWithValue("@numTarjetaCredito", (object?)pago.NumTarjetaCredito ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@nomUsuarioTarjeta", (object?)pago.NomUsuarioTarjeta ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idMetodoPago", pago.MetodoPago.IdMetodoPago);

            var newId = (int)cmd.ExecuteScalar();
            pago.IdPago = newId;
        }

    }
}
