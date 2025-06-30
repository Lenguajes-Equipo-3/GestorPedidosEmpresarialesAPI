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
    public class ClienteDataTest
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
                "DELETE FROM Cliente",
                "DBCC CHECKIDENT ('Cliente', RESEED, 0)",
                @"INSERT INTO Cliente (nombre_compania, nombre_contacto, apellido_contacto, puesto_contacto, direccion, ciudad, provincia, codigo_postal, pais, telefono, eliminado)
                  VALUES 
                  ('Empresa A', 'Juan', 'Pérez', 'Gerente', 'Calle 1', 'San José', 'San José', '10101', 'Costa Rica', '8888-0000', 0),
                  ('Empresa B', 'Ana', 'Gómez', 'Ventas', 'Avenida 2', 'Heredia', 'Heredia', '40101', 'Costa Rica', '8888-1111', 0)"
            };

            foreach (var cmdText in comandos)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private ClienteData CrearClienteData()
        {
            var configMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            return new ClienteData(configMock);
        }

        [Test]
        public void ObtenerTodos_DeberiaRetornarClientes()
        {
            var data = CrearClienteData();
            var lista = data.GetAllClientes();

            Assert.That(lista, Is.Not.Null);
            Assert.That(lista.Count, Is.GreaterThanOrEqualTo(2));
            Assert.That(lista[0].NombreCompania, Is.EqualTo("Empresa A"));
        }

        [Test]
        public void ObtenerPorId_ClienteExiste_DeberiaRetornarCliente()
        {
            var data = CrearClienteData();
            var cliente = data.GetClienteById(1);

            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.IdCliente, Is.EqualTo(1));
            Assert.That(cliente.NombreCompania, Is.EqualTo("Empresa A"));
        }

        [Test]
        public void ObtenerPorId_ClienteNoExiste_DeberiaRetornarNull()
        {
            var data = CrearClienteData();
            var cliente = data.GetClienteById(999);

            Assert.That(cliente, Is.Null);
        }

        [Test]
        public void Insertar_ClienteNuevo_DeberiaAgregarlo()
        {
            var data = CrearClienteData();

            var nuevo = new Cliente
            {
                NombreCompania = "Nueva Empresa",
                NombreContacto = "Carlos",
                ApellidoContacto = "Ramírez",
                PuestoContacto = "Compras",
                Direccion = "Ruta 32",
                Ciudad = "Limón",
                Provincia = "Limón",
                CodigoPostal = "70101",
                Pais = "Costa Rica",
                Telefono = "8888-3333"
            };

            data.Insert(nuevo);

            var clientes = data.GetAllClientes();
            Assert.That(clientes.Exists(c => c.NombreCompania == "Nueva Empresa"), Is.True);
        }

        [Test]
        public void Actualizar_ClienteExistente_DeberiaModificarDatos()
        {
            var data = CrearClienteData();

            var cliente = data.GetClienteById(1);
            cliente.NombreContacto = "Luis";
            cliente.PuestoContacto = "Subgerente";
            cliente.Telefono = "8888-9999";

            data.UpdateCliente(cliente);

            var actualizado = data.GetClienteById(1);
            Assert.That(actualizado.NombreContacto, Is.EqualTo("Luis"));
            Assert.That(actualizado.PuestoContacto, Is.EqualTo("Subgerente"));
            Assert.That(actualizado.Telefono, Is.EqualTo("8888-9999"));
        }

        [Test]
        public void Eliminar_ClienteExistente_DeberiaMarcarComoEliminado()
        {
            var data = CrearClienteData();

            data.DeleteCliente(1);
            var cliente = data.GetClienteById(1);

            Assert.That(cliente, Is.Null);
        }
    }
}
