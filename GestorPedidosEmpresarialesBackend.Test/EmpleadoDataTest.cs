using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class EmpleadoDataTest
    {
        private string _connectionString;
        private EmpleadoData _empleadoData;

        // Variables para almacenar los IDs generados en el Setup
        private int _idDeptoVentas;
        private int _idDeptoAdmin;
        private int _idRolVendedor;
        private int _idRolGerente;

        [SetUp]
        public async Task Setup()
        {
            _connectionString = "Data Source=163.178.173.130;Initial Catalog=GestorPedidosEmpresariales_Proyecto2;Persist Security Info=True;User ID=Lenguajes;Password=lenguajesparaiso2025;Encrypt=False;";

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", _connectionString }
                }).Build();

            _empleadoData = new EmpleadoData(configuration);

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // 1. Limpieza total en el orden correcto
            string[] cleanupCmds = {
                "DELETE FROM Usuario;",
                "DELETE FROM Empleado;",
                "DELETE FROM Departamento;",
                "DELETE FROM Rol;",
                "DBCC CHECKIDENT ('Usuario', RESEED, 0);",
                "DBCC CHECKIDENT ('Empleado', RESEED, 0);",
                "DBCC CHECKIDENT ('Departamento', RESEED, 0);",
                "DBCC CHECKIDENT ('Rol', RESEED, 0);"
            };
            foreach (var cmdText in cleanupCmds)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }

            // 2. Insertar datos maestros y capturar sus IDs
            using (var cmd = new SqlCommand("INSERT INTO Rol (nombre_rol, eliminado) OUTPUT INSERTED.id_rol VALUES ('Vendedor', 0);", connection))
            {
                _idRolVendedor = (int)await cmd.ExecuteScalarAsync();
            }
            using (var cmd = new SqlCommand("INSERT INTO Rol (nombre_rol, eliminado) OUTPUT INSERTED.id_rol VALUES ('Gerente', 0);", connection))
            {
                _idRolGerente = (int)await cmd.ExecuteScalarAsync();
            }
            using (var cmd = new SqlCommand("INSERT INTO Departamento (nombre_departamento, eliminado) OUTPUT INSERTED.id_departamento VALUES ('Ventas', 0);", connection))
            {
                _idDeptoVentas = (int)await cmd.ExecuteScalarAsync();
            }
            using (var cmd = new SqlCommand("INSERT INTO Departamento (nombre_departamento, eliminado) OUTPUT INSERTED.id_departamento VALUES ('Administración', 0);", connection))
            {
                _idDeptoAdmin = (int)await cmd.ExecuteScalarAsync();
            }

            // 3. Insertar empleado de prueba usando los IDs capturados
            string insertEmpleadoCmd = $"INSERT INTO Empleado (nombre_empleado, apellidos_empleado, id_departamento, id_rol, eliminado) VALUES ('Juan', 'Perez', {_idDeptoVentas}, {_idRolVendedor}, 0);";
            using (var cmd = new SqlCommand(insertEmpleadoCmd, connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        [Test]
        public void GetAll_DeberiaRetornarSoloLosEmpleadosActivos()
        {
            // Act
            var empleados = _empleadoData.GetAll();

            // Assert
            Assert.That(empleados.Count, Is.EqualTo(1));
        }

        [Test]
        public void Create_ConDatosValidos_DeberiaCrearEmpleadoYRetornarConId()
        {
            // Arrange
            var nuevoEmpleado = new Empleado
            {
                NombreEmpleado = "Carlos",
                ApellidosEmpleado = "Rodriguez",
                Puesto = "Vendedor Jr.",
                // Usamos los IDs capturados en el Setup
                Departamento = new Departamento { IdDepartamento = _idDeptoVentas },
                Rol = new Rol { IdRol = _idRolVendedor }
            };

            // Act
            var empleadoCreado = _empleadoData.Create(nuevoEmpleado);

            // Assert
            Assert.That(empleadoCreado, Is.Not.Null);
            Assert.That(empleadoCreado.IdEmpleado, Is.GreaterThan(0));

            // Verificamos que se pueda obtener desde la BD
            var empleadoDesdeBd = _empleadoData.GetById(empleadoCreado.IdEmpleado);
            Assert.That(empleadoDesdeBd, Is.Not.Null);
            Assert.That(empleadoDesdeBd.NombreEmpleado, Is.EqualTo("Carlos"));
            Assert.That(empleadoDesdeBd.Departamento.IdDepartamento, Is.EqualTo(_idDeptoVentas));
        }
    }
}