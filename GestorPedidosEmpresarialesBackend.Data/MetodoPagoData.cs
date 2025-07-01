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
    public class MetodoPagoData
    {
        private readonly string connectionString;

        public MetodoPagoData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<MetodoPago> GetAll()
        {
            var lista = new List<MetodoPago>();

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM MetodoPago WHERE eliminado = 0";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new MetodoPago
                {
                    IdMetodoPago = reader.GetInt32(reader.GetOrdinal("id_metodo_pago")),
                    Metodo = reader.GetString(reader.GetOrdinal("metodo_pago")),
                    TarjetaCredito = reader.GetString(reader.GetOrdinal("tarjeta_credito")),
                    Eliminado = false
                });
            }

            return lista;
        }

        public MetodoPago? GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM MetodoPago WHERE id_metodo_pago = @id AND eliminado = 0";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new MetodoPago
                {
                    IdMetodoPago = reader.GetInt32(reader.GetOrdinal("id_metodo_pago")),
                    Metodo = reader.GetString(reader.GetOrdinal("metodo_pago")),
                    TarjetaCredito = reader.GetString(reader.GetOrdinal("tarjeta_credito")),
                    Eliminado = false
                };
            }

            return null;
        }
    }
}
