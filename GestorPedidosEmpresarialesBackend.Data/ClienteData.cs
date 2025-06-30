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
    public class ClienteData
    {
        private readonly string connectionString;

        public ClienteData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Cliente> GetAllClientes()
        {
            var lista = new List<Cliente>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Cliente WHERE eliminado = 0";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var cliente = new Cliente
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
                    Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                    Eliminado = false
                };

                lista.Add(cliente);
            }

            return lista;
        }

        public void Insert(Cliente cliente)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = @"
                INSERT INTO Cliente 
                (nombre_compania, nombre_contacto, apellido_contacto, puesto_contacto,
                 direccion, ciudad, provincia, codigo_postal, pais, telefono, eliminado)
                VALUES (@compania, @nombre, @apellido, @puesto, @direccion, @ciudad,
                        @provincia, @codigo, @pais, @telefono, 0)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@compania", cliente.NombreCompania);
            command.Parameters.AddWithValue("@nombre", cliente.NombreContacto);
            command.Parameters.AddWithValue("@apellido", cliente.ApellidoContacto);
            command.Parameters.AddWithValue("@puesto", cliente.PuestoContacto);
            command.Parameters.AddWithValue("@direccion", cliente.Direccion);
            command.Parameters.AddWithValue("@ciudad", cliente.Ciudad);
            command.Parameters.AddWithValue("@provincia", cliente.Provincia);
            command.Parameters.AddWithValue("@codigo", cliente.CodigoPostal);
            command.Parameters.AddWithValue("@pais", cliente.Pais);
            command.Parameters.AddWithValue("@telefono", cliente.Telefono);

            command.ExecuteNonQuery();
        }

        public Cliente? GetClienteById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM Cliente WHERE id_cliente = @id AND eliminado = 0";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
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
                    Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                    Eliminado = false
                };
            }

            return null;
        }

        public void UpdateCliente (Cliente cliente)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                    UPDATE Cliente SET
                        nombre_compania = @compania,
                        nombre_contacto = @nombre,
                        apellido_contacto = @apellido,
                        puesto_contacto = @puesto,
                        direccion = @direccion,
                        ciudad = @ciudad,
                        provincia = @provincia,
                        codigo_postal = @codigo,
                        pais = @pais,
                        telefono = @telefono
                    WHERE id_cliente = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", cliente.IdCliente);
            command.Parameters.AddWithValue("@compania", cliente.NombreCompania);
            command.Parameters.AddWithValue("@nombre", cliente.NombreContacto);
            command.Parameters.AddWithValue("@apellido", cliente.ApellidoContacto);
            command.Parameters.AddWithValue("@puesto", cliente.PuestoContacto);
            command.Parameters.AddWithValue("@direccion", cliente.Direccion);
            command.Parameters.AddWithValue("@ciudad", cliente.Ciudad);
            command.Parameters.AddWithValue("@provincia", cliente.Provincia);
            command.Parameters.AddWithValue("@codigo", cliente.CodigoPostal);
            command.Parameters.AddWithValue("@pais", cliente.Pais);
            command.Parameters.AddWithValue("@telefono", cliente.Telefono);

            command.ExecuteNonQuery();
        }

        public void  DeleteCliente(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "UPDATE Cliente SET eliminado = 1 WHERE id_cliente = @id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }


    }
}
