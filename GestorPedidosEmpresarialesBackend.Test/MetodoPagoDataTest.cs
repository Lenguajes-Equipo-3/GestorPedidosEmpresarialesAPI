using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class MetodoPagoDataTest
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
                "DELETE FROM MetodoPago",
                "DBCC CHECKIDENT ('MetodoPago', RESEED, 0)",
                @"INSERT INTO MetodoPago (metodo_pago, tarjeta_credito, eliminado) VALUES 
                    ('Efectivo', '', 0),
                    ('Tarjeta', 'Visa', 0),
                    ('Transferencia', '', 1)" //este no debe aparecer
            };

            foreach (var cmdText in comandos)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private MetodoPagoData CrearData()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            return new MetodoPagoData(config);
        }

        [Test]
        public void GetAll_DeberiaRetornarSoloActivos()
        {
            var data = CrearData();
            var lista = data.GetAll();

            Assert.That(lista, Is.Not.Null);
            Assert.That(lista.Count, Is.EqualTo(2));
            Assert.That(lista.Exists(m => m.Metodo == "Efectivo"), Is.True);
            Assert.That(lista.Exists(m => m.Metodo == "Transferencia"), Is.False); // Eliminado
        }

        [Test]
        public void GetById_Existe_DeberiaRetornarMetodo()
        {
            var data = CrearData();
            var metodo = data.GetById(1);

            Assert.That(metodo, Is.Not.Null);
            Assert.That(metodo.Metodo, Is.EqualTo("Efectivo"));
        }

        [Test]
        public void GetById_NoExiste_DeberiaRetornarNull()
        {
            var data = CrearData();
            var metodo = data.GetById(999);

            Assert.That(metodo, Is.Null);
        }
    }
}
