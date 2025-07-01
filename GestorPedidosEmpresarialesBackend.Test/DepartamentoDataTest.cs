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
    public class DepartamentoDataTest
    {
        private string connectionString;
        private DepartamentoData departamentoData;

        [SetUp]
        public async Task Setup()
        {
            connectionString = "Data Source=163.178.173.130;Initial Catalog=GestorPedidosEmpresariales_Proyecto2;Persist Security Info=True;User ID=Lenguajes;Password=lenguajesparaiso2025;Encrypt=False;";
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "ConnectionStrings:DefaultConnection", connectionString } }).Build();
            departamentoData = new DepartamentoData(configuration);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string[] commands =
            {
        "DELETE FROM Usuario;",
        "DELETE FROM Empleado;",
        "DELETE FROM Departamento;",
        "DBCC CHECKIDENT ('Departamento', RESEED, 0);",
        "INSERT INTO Departamento (nombre_departamento, eliminado) VALUES ('Ventas', 0);",
        "INSERT INTO Departamento (nombre_departamento, eliminado) VALUES ('Tecnología', 0);"
    };

            foreach (var cmdText in commands)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        [Test]
        public void GetAll_CuandoSeInvoca_DeberiaRetornarTodosLosDepartamentosActivos()
        {
            // Act
            var departamentos = departamentoData.GetAll();

            // Assert
            Assert.That(departamentos.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetById_CuandoIdExiste_DeberiaRetornarDepartamentoCorrecto()
        {
            // Act
            var deptoVentas = departamentoData.GetAll().First(d => d.NombreDepartamento == "Ventas");
            var deptoDesdeBd = departamentoData.GetById(deptoVentas.IdDepartamento);

            // Assert
            Assert.That(deptoDesdeBd, Is.Not.Null);
            Assert.That(deptoDesdeBd.IdDepartamento, Is.EqualTo(deptoVentas.IdDepartamento));
            Assert.That(deptoDesdeBd.NombreDepartamento, Is.EqualTo("Ventas"));
        }
    }
}