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

        // Método auxiliar para leer strings que pueden ser NULL
        private string? GetNullableString(SqlDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
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
                    NombreCompania = GetNullableString(reader, "nombre_compania"),
                    NombreContacto = GetNullableString(reader, "nombre_contacto"),
                    ApellidoContacto = GetNullableString(reader, "apellido_contacto"),
                    PuestoContacto = GetNullableString(reader, "puesto_contacto"),
                    Direccion = GetNullableString(reader, "direccion"),
                    Ciudad = GetNullableString(reader, "ciudad"),
                    Provincia = GetNullableString(reader, "provincia"),
                    CodigoPostal = GetNullableString(reader, "codigo_postal"),
                    Pais = GetNullableString(reader, "pais"),
                    Telefono = GetNullableString(reader, "telefono"),
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
            command.Parameters.AddWithValue("@compania", (object?)cliente.NombreCompania ?? DBNull.Value);
            command.Parameters.AddWithValue("@nombre", (object?)cliente.NombreContacto ?? DBNull.Value);
            command.Parameters.AddWithValue("@apellido", (object?)cliente.ApellidoContacto ?? DBNull.Value);
            command.Parameters.AddWithValue("@puesto", (object?)cliente.PuestoContacto ?? DBNull.Value);
            command.Parameters.AddWithValue("@direccion", (object?)cliente.Direccion ?? DBNull.Value);
            command.Parameters.AddWithValue("@ciudad", (object?)cliente.Ciudad ?? DBNull.Value);
            command.Parameters.AddWithValue("@provincia", (object?)cliente.Provincia ?? DBNull.Value);
            command.Parameters.AddWithValue("@codigo", (object?)cliente.CodigoPostal ?? DBNull.Value);
            command.Parameters.AddWithValue("@pais", (object?)cliente.Pais ?? DBNull.Value);
            command.Parameters.AddWithValue("@telefono", (object?)cliente.Telefono ?? DBNull.Value);

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
                    NombreCompania = GetNullableString(reader, "nombre_compania"),
                    NombreContacto = GetNullableString(reader, "nombre_contacto"),
                    ApellidoContacto = GetNullableString(reader, "apellido_contacto"),
                    PuestoContacto = GetNullableString(reader, "puesto_contacto"),
                    Direccion = GetNullableString(reader, "direccion"),
                    Ciudad = GetNullableString(reader, "ciudad"),
                    Provincia = GetNullableString(reader, "provincia"),
                    CodigoPostal = GetNullableString(reader, "codigo_postal"),
                    Pais = GetNullableString(reader, "pais"),
                    Telefono = GetNullableString(reader, "telefono"),
                    Eliminado = false
                };
            }

            return null;
        }

        public void UpdateCliente(Cliente cliente)
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
            command.Parameters.AddWithValue("@compania", (object?)cliente.NombreCompania ?? DBNull.Value);
            command.Parameters.AddWithValue("@nombre", (object?)cliente.NombreContacto ?? DBNull.Value);
            command.Parameters.AddWithValue("@apellido", (object?)cliente.ApellidoContacto ?? DBNull.Value);
            command.Parameters.AddWithValue("@puesto", (object?)cliente.PuestoContacto ?? DBNull.Value);
            command.Parameters.AddWithValue("@direccion", (object?)cliente.Direccion ?? DBNull.Value);
            command.Parameters.AddWithValue("@ciudad", (object?)cliente.Ciudad ?? DBNull.Value);
            command.Parameters.AddWithValue("@provincia", (object?)cliente.Provincia ?? DBNull.Value);
            command.Parameters.AddWithValue("@codigo", (object?)cliente.CodigoPostal ?? DBNull.Value);
            command.Parameters.AddWithValue("@pais", (object?)cliente.Pais ?? DBNull.Value);
            command.Parameters.AddWithValue("@telefono", (object?)cliente.Telefono ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void DeleteCliente(int id)
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
