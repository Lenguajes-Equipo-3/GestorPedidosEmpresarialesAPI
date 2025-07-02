using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class OrdenDataTest
    {
       private string connectionString;
       private OrdenData ordenData;
        private int idCliente, idEmpleado, idProductoBase, idVariante, idCategoria, idDepartamento;

        [SetUp]
        public async Task Setup()
        {
            connectionString = "Data Source=163.178.173.130;Initial Catalog=GestorPedidosEmpresariales_Proyecto2;Persist Security Info=True;User ID=Lenguajes;Password=lenguajesparaiso2025;Encrypt=False;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Limpiar las tablas involucradas
                string[] comandos = new string[]
                {

                    "DELETE FROM Usuario",
                    "DBCC CHECKIDENT ('Usuario', RESEED, 0)",
                    "DELETE FROM DetalleOrden",
                    "DBCC CHECKIDENT ('DetalleOrden', RESEED, 0)",
                    "DELETE FROM Orden",
                    "DBCC CHECKIDENT ('Orden', RESEED, 0)",
                    "DELETE FROM VarianteProducto",
                    "DBCC CHECKIDENT ('VarianteProducto', RESEED, 0)",
                    "DELETE FROM ProductoBase",
                    "DBCC CHECKIDENT ('ProductoBase', RESEED, 0)",
                    "DELETE FROM Cliente",
                    "DBCC CHECKIDENT ('Cliente', RESEED, 0)",
                    "DELETE FROM Empleado",
                    "DBCC CHECKIDENT ('Empleado', RESEED, 0)",
                    "DELETE FROM Departamento",
                    "DBCC CHECKIDENT ('Departamento', RESEED, 0)",
                    "DELETE FROM Categoria",
                    "DBCC CHECKIDENT ('Categoria', RESEED, 0)",
                };

                foreach (var cmdText in comandos)
                {
                    using var cmd = new SqlCommand(cmdText, connection);
                    await cmd.ExecuteNonQueryAsync();
                }

                // Insertar datos mínimos necesarios y guardar los IDs generados
                using (var cmd = new SqlCommand(@"
                    INSERT INTO Categoria (descripcion, eliminado) VALUES ('CategoriaTest', 0);
                    SELECT SCOPE_IDENTITY();", connection))
                    idCategoria = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                using (var cmd = new SqlCommand(@"
                    INSERT INTO Departamento (nombre_departamento, eliminado) VALUES ('DepTest', 0);
                    SELECT SCOPE_IDENTITY();", connection))
                    idDepartamento = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                using (var cmd = new SqlCommand(@"
                    INSERT INTO Empleado (nombre_empleado, apellidos_empleado, puesto, id_departamento, eliminado)
                    VALUES ('Juan', 'Pérez', 'Vendedor', @idDepartamento, 0);
                    SELECT SCOPE_IDENTITY();", connection))
                {
                    cmd.Parameters.AddWithValue("@idDepartamento", idDepartamento);
                    idEmpleado = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                using (var cmd = new SqlCommand(@"
                    INSERT INTO Cliente (nombre_compania, nombre_contacto, apellido_contacto, direccion, telefono, eliminado)
                    VALUES ('EmpresaTest', 'Ana', 'López', 'Calle 1', '8888-1111', 0);
                    SELECT SCOPE_IDENTITY();", connection))
                    idCliente = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                using (var cmd = new SqlCommand(@"
                    INSERT INTO ProductoBase (nombre_producto, id_categoria, eliminado)
                    VALUES ('ProdTest', @idCategoria, 0);
                    SELECT SCOPE_IDENTITY();", connection))
                {
                    cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                    idProductoBase = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                using (var cmd = new SqlCommand(@"
                    INSERT INTO VarianteProducto (id_producto_base, talla, descripcion, precio, cantidad_existencias, eliminado)
                    VALUES (@idProductoBase, 'M', 'Variante test', 1500, 100, 0);
                    SELECT SCOPE_IDENTITY();", connection))
                {
                    cmd.Parameters.AddWithValue("@idProductoBase", idProductoBase);
                    idVariante = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }

            // Preparar la instancia de OrdenData con la configuración simulada
            var configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();
            ordenData = new OrdenData(configurationMock);
        }

        [Test]
        public void InsertarOrdenConDetallesYObtenerOrdenes_Success()
        {
            // Arrange
            var orden = new Orden
            {
                Cliente = new Cliente { IdCliente = idCliente },
                Empleado = new Empleado { IdEmpleado = idEmpleado },
                FechaOrden = DateTime.Now,
                DireccionViaje = "Test Direccion",
                CiudadViaje = "Test Ciudad",
                ProvinciaViaje = "Test Provincia",
                PaisViaje = "Test Pais",
                TelefonoViaje = "9999-8888",
                Eliminado = false,
                DetallesOrden = new List<DetalleOrden>
                {
                    new DetalleOrden(
                        idDetalleOrden: 0,
                        varianteProducto: new VarianteProducto { IdVariante = idVariante },
                        cantidad: 2,
                        precioLinea: 1500,
                        eliminado: false)
                }
            };

            // Act
            int idOrden = ordenData.InsertarOrdenConDetalles(orden);
            var ordenes = ordenData.ObtenerOrdenes();

            // Assert
            Assert.That(ordenes, Is.Not.Empty, "Debe haber órdenes registradas.");
            var ordenInsertada = ordenes.Find(o => o.IdOrden == idOrden);
            Assert.That(ordenInsertada,Is.Not.Null,"No se recuperó la orden insertada.");
            Assert.That("Test Direccion", Is.Not.Null, ordenInsertada.DireccionViaje);
            Assert.That(ordenInsertada.DetallesOrden, Is.Not.Empty, "Debe haber detalles en la orden.");
            Assert.That(2 == ordenInsertada.DetallesOrden[0].Cantidad);
        }
    }
}
