using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class CategoriaDataTest
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
                "DELETE FROM Categoria",
                "DBCC CHECKIDENT ('Categoria', RESEED, 0)",
                @"INSERT INTO Categoria (descripcion, eliminado)
                  VALUES
                  ('Papelería', 0),
                  ('Electrónica', 0)"
            };

            foreach (var cmdText in comandos)
            {
                using var cmd = new SqlCommand(cmdText, connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private CategoriaData CrearCategoriaData()
        {
            var configMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            return new CategoriaData(configMock);
        }

        [Test]
        public void ObtenerTodos_DeberiaRetornarCategorias()
        {
            var data = CrearCategoriaData();
            List<Categoria> lista = data.GetAllCategorias();

            Assert.That(lista, Is.Not.Null);
            Assert.That(lista.Count, Is.GreaterThanOrEqualTo(2));
            Assert.That(lista.Exists(c => c.Descripcion == "Papelería"), Is.True);
        }

        [Test]
        public void ObtenerPorId_CategoriaExiste_DeberiaRetornarCategoria()
        {
            var data = CrearCategoriaData();
            Categoria categoria = data.GetCategoriaById(1);

            Assert.That(categoria, Is.Not.Null);
            Assert.That(categoria.IdCategoria, Is.EqualTo(1));
            Assert.That(categoria.Descripcion, Is.EqualTo("Papelería"));
        }

        [Test]
        public void ObtenerPorId_CategoriaNoExiste_DeberiaRetornarNull()
        {
            var data = CrearCategoriaData();
            Categoria categoria = data.GetCategoriaById(999);

            Assert.That(categoria, Is.Null);
        }

        [Test]
        public void Insertar_CategoriaNueva_DeberiaAgregarla()
        {
            var data = CrearCategoriaData();

            var nueva = new Categoria(0, "Higiene", false);

            data.InsertCategoria(nueva);

            var categorias = data.GetAllCategorias();
            Assert.That(categorias.Exists(c => c.Descripcion == "Higiene"), Is.True);
        }

        [Test]
        public void Eliminar_CategoriaExistente_DeberiaMarcarComoEliminado()
        {
            var data = CrearCategoriaData();

            data.DeleteCategoria(1);
            var categoria = data.GetCategoriaById(1);

            Assert.That(categoria, Is.Null);
        }
    }
}
