using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class RolDataTest
    {
        private string connectionString;
        private RolData rolData;

        [SetUp]
        public async Task Setup()
        {
            connectionString = "Data Source=163.178.173.130;Initial Catalog=GestorPedidosEmpresariales_Proyecto2;Persist Security Info=True;User ID=Lenguajes;Password=lenguajesparaiso2025;Encrypt=False;";
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "ConnectionStrings:DefaultConnection", connectionString } }).Build();
            rolData = new RolData(configuration);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string[] cleanupCommands =
            {
        "DELETE FROM Usuario;",
        "DELETE FROM Empleado;",
        "DELETE FROM Rol;",
        "DBCC CHECKIDENT ('Rol', RESEED, 0);",
        "INSERT INTO Rol (nombre_rol, eliminado) VALUES ('Administrador', 0);",
        "INSERT INTO Rol (nombre_rol, eliminado) VALUES ('Vendedor', 0);"
    };

            foreach (var cmdText in cleanupCommands)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        [Test]
        public void GetAll_CuandoSeInvoca_DeberiaRetornarTodosLosRolesActivos()
        {
            // Act
            var roles = rolData.GetAll();

            // Assert
            Assert.That(roles.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetById_CuandoIdExiste_DeberiaRetornarRolCorrecto()
        {
            // Act
            var rolAdmin = rolData.GetAll().First(r => r.NombreRol == "Administrador");
            var rolDesdeBd = rolData.GetById(rolAdmin.IdRol);

            // Assert
            Assert.That(rolDesdeBd, Is.Not.Null);
            Assert.That(rolDesdeBd.IdRol, Is.EqualTo(rolAdmin.IdRol));
            Assert.That(rolDesdeBd.NombreRol, Is.EqualTo("Administrador"));
        }
    }
}