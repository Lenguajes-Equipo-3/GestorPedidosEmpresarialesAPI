namespace GestorPedidosEmpresarialesBackend.Test;

using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

public class OrdenDataTests
{
    private readonly string _connectionString;
    private readonly OrdenData _ordenData;

    public OrdenDataTests()
    {
        // Puede usar appsettings.Development.json o pasar la cadena aquí para el test
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _ordenData = new OrdenData(configuration);

        LimpiarTablas();
        InsertarDatosBase();
    }

    private void LimpiarTablas()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var comandos = new[]
            {
                "DELETE FROM DetalleOrden",
                "DELETE FROM Orden",
                "DELETE FROM VarianteProducto",
                "DELETE FROM ProductoBase",
                "DELETE FROM Cliente",
                "DELETE FROM Empleado",
                "DELETE FROM Departamento",
                "DELETE FROM Categoria"
            };

            foreach (var cmdText in comandos)
            {
                using (var cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    private int InsertarCategoria()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Categoria (descripcion, eliminado) VALUES ('Categoria Test', 0); SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(sql, connection))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private int InsertarDepartamento()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Departamento (nombre_departamento, eliminado) VALUES ('Departamento Test', 0); SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(sql, connection))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private int InsertarEmpleado(int idDepartamento)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Empleado (nombre_empleado, apellidos_empleado, puesto, id_departamento, eliminado) " +
                         "VALUES ('Juan', 'Pérez', 'Vendedor', @idDepartamento, 0); SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@idDepartamento", idDepartamento);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private int InsertarCliente()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Cliente (nombre_compania, nombre_contacto, apellido_contacto, direccion, telefono, eliminado) " +
                         "VALUES ('Empresa Test', 'Ana', 'Lopez', 'Calle 123', '8888-8888', 0); SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(sql, connection))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private int InsertarProductoBase(int idCategoria)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO ProductoBase (nombre_producto, id_categoria, eliminado) VALUES ('Producto Test', @idCategoria, 0); SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private int InsertarVarianteProducto(int idProductoBase)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO VarianteProducto (id_producto_base, talla, descripcion, precio, cantidad_existencias, eliminado) " +
                         "VALUES (@idProductoBase, 'M', 'Variante test', 1500, 10, 0); SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@idProductoBase", idProductoBase);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    private void InsertarDatosBase()
    {
        // Inserta todos los datos necesarios para una orden de prueba.
        int idCategoria = InsertarCategoria();
        int idDepartamento = InsertarDepartamento();
        int idEmpleado = InsertarEmpleado(idDepartamento);
        int idCliente = InsertarCliente();
        int idProductoBase = InsertarProductoBase(idCategoria);
        int idVariante = InsertarVarianteProducto(idProductoBase);

        // Guarda los ids para usarlos en el test
        TestContext = new TestDataContext
        {
            IdCategoria = idCategoria,
            IdDepartamento = idDepartamento,
            IdEmpleado = idEmpleado,
            IdCliente = idCliente,
            IdProductoBase = idProductoBase,
            IdVariante = idVariante
        };
    }

    public class TestDataContext
    {
        public int IdCategoria { get; set; }
        public int IdDepartamento { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdProductoBase { get; set; }
        public int IdVariante { get; set; }
    }

    public TestDataContext TestContext { get; private set; }

    [Fact]
    public void InsertarYObtenerOrden_Success()
    {
        // Arrange
        var orden = new Orden
        {
            Cliente = new Cliente { IdCliente = TestContext.IdCliente },
            Empleado = new Empleado { IdEmpleado = TestContext.IdEmpleado },
            FechaOrden = DateTime.Now,
            DireccionViaje = "Dirección prueba",
            CiudadViaje = "Ciudad prueba",
            ProvinciaViaje = "Provincia prueba",
            PaisViaje = "Pais prueba",
            TelefonoViaje = "8888-9999",
            Eliminado = false,
            DetallesOrden = new List<DetalleOrden>
            {
                new DetalleOrden(
                    idDetalleOrden: 0,
                    varianteProducto: new VarianteProducto { IdVariante = TestContext.IdVariante },
                    cantidad: 2,
                    precioLinea: 1500,
                    eliminado: false)
            }
        };

        // Act
        int idOrden = _ordenData.InsertarOrdenConDetalles(orden);
        var ordenes = _ordenData.ObtenerOrdenes();

        // Assert
        Assert.Empty(ordenes);
        var ordenRecuperada = ordenes.Find(o => o.IdOrden == idOrden);
        Assert.NotNull(ordenRecuperada);
        Assert.Equal("Dirección prueba", ordenRecuperada.DireccionViaje);
        Assert.NotEmpty(ordenRecuperada.DetallesOrden);
        Assert.Equal(2, ordenRecuperada.DetallesOrden[0].Cantidad);
    }
}
