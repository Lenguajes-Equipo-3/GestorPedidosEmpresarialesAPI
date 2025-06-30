using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class ParametrosSistemaDataTest
    {
        private string connectionString;

        [SetUp]
        public async Task Setup()
        {
            connectionString = "Data Source=163.178.173.130;Initial Catalog=GestorPedidosEmpresariales_Proyecto2;Persist Security Info=True;User ID=Lenguajes;Password=lenguajesparaiso2025;Encrypt=False;";

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string[] comandos = new string[]
            {
                "DELETE FROM ParametrosSistema",
                "DBCC CHECKIDENT ('ParametrosSistema', RESEED, 0)",

                @"INSERT INTO ParametrosSistema 
                    (nombre_empresa, direccion, telefono, correo, impuesto_ventas, logo, moneda, ultima_actualizacion)
                  VALUES 
                    ('Mi Empresa', 'Calle Central', '2222-2222', 'info@empresa.com', 13.0, NULL, 'CRC', GETDATE())"
            };

            foreach (var cmdText in comandos)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private ParametrosSistemaData CrearData()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            return new ParametrosSistemaData(config);
        }

        [Test]
        public void Obtener_DeberiaRetornarParametros()
        {
            var data = CrearData();

            var parametros = data.GetParametros();

            Assert.That(parametros, Is.Not.Null);
            Assert.That(parametros.NombreEmpresa, Is.EqualTo("Mi Empresa"));
        }

        [Test]
        public void Actualizar_DeberiaModificarDatosCorrectamente()
        {
            var data = CrearData();
            var parametros = data.GetParametros();

            parametros.NombreEmpresa = "Empresa Modificada";
            parametros.Telefono = "8888-8888";
            parametros.ImpuestoVentas = 15;

            data.UpdateParametros(parametros);

            var actualizados = data.GetParametros();

            Assert.That(actualizados.NombreEmpresa, Is.EqualTo("Empresa Modificada"));
            Assert.That(actualizados.Telefono, Is.EqualTo("8888-8888"));
            Assert.That(actualizados.ImpuestoVentas, Is.EqualTo(15));
        }

        [Test]
        public void Agregar_CuandoNoExiste_DeberiaInsertarParametros()
        {
            // Limpia la tabla para forzar inserción
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using (var cmd = new SqlCommand("DELETE FROM ParametrosSistema", connection))
            {
                cmd.ExecuteNonQuery();
            }

            var data = CrearData();

            var nuevo = new ParametrosSistema
            {
                NombreEmpresa = "Primera Empresa",
                Direccion = "Calle Nueva",
                Telefono = "5555-1234",
                Correo = "contacto@nueva.com",
                ImpuestoVentas = 10,
                Moneda = "CRC",
                Logo = null
            };

            data.AddParametro(nuevo);

            var actual = data.GetParametros();
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.NombreEmpresa, Is.EqualTo("Primera Empresa"));
        }
    }
}

