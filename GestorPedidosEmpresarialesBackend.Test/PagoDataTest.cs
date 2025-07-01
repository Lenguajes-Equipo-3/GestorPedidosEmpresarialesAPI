using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class PagoDataTest
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
                "DELETE FROM Pago",
                "DBCC CHECKIDENT ('Pago', RESEED, 0)",

                "DELETE FROM Usuario", // Primero eliminar los usuarios para no romper FK con Empleado

                "DELETE FROM Orden",
                "DBCC CHECKIDENT ('Orden', RESEED, 0)",

                "DELETE FROM Cliente",
                "DBCC CHECKIDENT ('Cliente', RESEED, 0)",

                "DELETE FROM Empleado",
                "DBCC CHECKIDENT ('Empleado', RESEED, 0)",

                "DELETE FROM Departamento",
                "DBCC CHECKIDENT ('Departamento', RESEED, 0)",

                "DELETE FROM Rol",
                "DBCC CHECKIDENT ('Rol', RESEED, 0)",

                "DELETE FROM MetodoPago",
                "DBCC CHECKIDENT ('MetodoPago', RESEED, 0)",

              
                "INSERT INTO Departamento (nombre_departamento) VALUES ('Ventas')",

                "INSERT INTO Rol (nombre_rol) VALUES ('Administrador')",

             
                @"INSERT INTO Empleado (nombre_empleado, apellidos_empleado, puesto, extension, telefono_trabajo, id_departamento, id_rol, eliminado)
                  VALUES ('Mario', 'Rojas', 'Supervisor', '101', '2222-3333', 1, 1, 0)",

               
                @"INSERT INTO Cliente (nombre_compania, nombre_contacto, apellido_contacto, puesto_contacto, direccion, ciudad, provincia, codigo_postal, pais, telefono, eliminado)
                  VALUES ('Cliente Test', 'Laura', 'Sánchez', 'Gerente', 'Calle 100', 'San José', 'San José', '10101', 'Costa Rica', '8888-0000', 0)",

                
                @"INSERT INTO Orden (id_cliente, id_empleado, fecha_orden, direccion_viaje, ciudad_viaje, provincia_viaje, pais_viaje, telefono_viaje, eliminado)
                  VALUES (1, 1, GETDATE(), 'Destino X', 'Cartago', 'Cartago', 'Costa Rica', '2222-0000', 0)",

                @"INSERT INTO MetodoPago (metodo_pago, tarjeta_credito, eliminado)
                  VALUES ('Tarjeta de crédito', '1234567890123456', 0)"
            };

            foreach (var cmdText in comandos)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private PagoData CrearPagoData()
        {
            var configMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            return new PagoData(configMock);
        }

        [Test]
        public void InsertarPago_DeberiaAgregarCorrectamente()
        {
            var data = CrearPagoData();

            var nuevoPago = new Pago
            {
                CantidadPago = 15000,
                FechaPago = DateTime.Now,
                NumTarjetaCredito = "5555444433332222",
                NomUsuarioTarjeta = "Test Pago",
                Orden = new Orden { IdOrden = 1 },
                MetodoPago = new MetodoPago { IdMetodoPago = 1 },
                Eliminado = false
            };

            data.Insert(nuevoPago);

            Assert.That(nuevoPago.IdPago, Is.GreaterThan(0));

            var lista = data.GetAll();

            Assert.That(lista.Any(p => p.IdPago == nuevoPago.IdPago), Is.True);
            Assert.That(lista.Any(p => p.NomUsuarioTarjeta == "Test Pago"), Is.True);
        }

        [Test]
        public void ObtenerTodos_DeberiaRetornarPagos()
        {
            var data = CrearPagoData();

            var lista = data.GetAll();

            Assert.That(lista, Is.Not.Null);
            Assert.That(lista.Count, Is.GreaterThanOrEqualTo(0)); 
        }
    }
}
