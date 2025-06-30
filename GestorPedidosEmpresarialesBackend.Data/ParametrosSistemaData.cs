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
    public class ParametrosSistemaData
    {
        private readonly string connectionString;

        public ParametrosSistemaData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public void AddParametro (ParametrosSistema parametros)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
        INSERT INTO ParametrosSistema 
        (nombre_empresa, direccion, telefono, correo, impuesto_ventas, logo, moneda, ultima_actualizacion)
        VALUES 
        (@nombre, @direccion, @telefono, @correo, @impuesto, @logo, @moneda, GETDATE())";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nombre", parametros.NombreEmpresa);
            cmd.Parameters.AddWithValue("@direccion", parametros.Direccion);
            cmd.Parameters.AddWithValue("@telefono", parametros.Telefono);
            cmd.Parameters.AddWithValue("@correo", parametros.Correo);
            cmd.Parameters.AddWithValue("@impuesto", parametros.ImpuestoVentas);
            cmd.Parameters.AddWithValue("@moneda", parametros.Moneda);
            cmd.Parameters.Add("@logo", System.Data.SqlDbType.VarBinary).Value =
                (object?)parametros.Logo ?? DBNull.Value;

            cmd.ExecuteNonQuery();
        }
        public ParametrosSistema? GetParametros()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT TOP 1 * FROM ParametrosSistema ORDER BY id_parametro DESC";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new ParametrosSistema
                {
                    IdParametro = reader.GetInt32(reader.GetOrdinal("id_parametro")),
                    NombreEmpresa = reader.GetString(reader.GetOrdinal("nombre_empresa")),
                    Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                    Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                    Correo = reader.GetString(reader.GetOrdinal("correo")),
                    ImpuestoVentas = reader.GetDecimal(reader.GetOrdinal("impuesto_ventas")),
                    Moneda = reader.GetString(reader.GetOrdinal("moneda")),
                    UltimaActualizacion = reader.GetDateTime(reader.GetOrdinal("ultima_actualizacion")),
                    Logo = reader.IsDBNull(reader.GetOrdinal("logo")) ? null : (byte[])reader["logo"]
                };
            }

            return null;
        }

        public void UpdateParametros(ParametrosSistema parametros)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
        UPDATE ParametrosSistema SET
            nombre_empresa = @nombre,
            direccion = @direccion,
            telefono = @telefono,
            correo = @correo,
            impuesto_ventas = @impuesto,
            logo = @logo,
            moneda = @moneda,
            ultima_actualizacion = GETDATE()
        WHERE id_parametro = @id";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", parametros.IdParametro);
            cmd.Parameters.AddWithValue("@nombre", parametros.NombreEmpresa);
            cmd.Parameters.AddWithValue("@direccion", parametros.Direccion);
            cmd.Parameters.AddWithValue("@telefono", parametros.Telefono);
            cmd.Parameters.AddWithValue("@correo", parametros.Correo);
            cmd.Parameters.AddWithValue("@impuesto", parametros.ImpuestoVentas);
            cmd.Parameters.AddWithValue("@moneda", parametros.Moneda);
            cmd.Parameters.Add("@logo", System.Data.SqlDbType.VarBinary).Value =
                (object?)parametros.Logo ?? DBNull.Value;

            cmd.ExecuteNonQuery();
        }
    }
}
