using GestorPedidosEmpresarialesBackend.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace GestorPedidosEmpresarialesBackend.Test
{
    [TestFixture]
    public class UsuarioDataTest
    {
        private string connectionString;

        [SetUp]
        public async Task Setup()
        {
            // Ajustá la cadena de conexión a tu entorno local
            connectionString = "Data Source=Yarex\\SQLEXPRESS;Initial Catalog=GestorPedidosEmpresariales_Proyecto2;Integrated Security=True;Encrypt=False";
            ; using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Limpiar las tablas en orden por dependencias
                string[] comandos = new string[]
                {
                    "DELETE FROM Usuario",
                    "DBCC CHECKIDENT ('Usuario', RESEED, 0)",
                    "DELETE FROM Empleado",
                    "DBCC CHECKIDENT ('Empleado', RESEED, 0)",
                    "DELETE FROM Departamento",
                    "DBCC CHECKIDENT ('Departamento', RESEED, 0)",
                    "DELETE FROM Rol",
                    "DBCC CHECKIDENT ('Rol', RESEED, 0)",

                    // Insertar datos de prueba
                    "INSERT INTO Rol (nombre_rol) VALUES ('Administrador'), ('Vendedor'), ('Soporte Técnico')",
                    "INSERT INTO Departamento (nombre_departamento) VALUES ('Ventas'), ('Tecnología'), ('Recursos Humanos')",
                    @"INSERT INTO Empleado (nombre_empleado, apellidos_empleado, puesto, extension, telefono_trabajo, id_departamento, id_rol)
                      VALUES 
                      ('Laura', 'Gómez Sánchez', 'Ejecutiva de ventas', '101', '8888-1111', 1, 2),
                      ('Carlos', 'Morales Rojas', 'Ingeniero en sistemas', '202', '8888-2222', 2, 3),
                      ('Ana', 'Pérez Méndez', 'Coordinadora de RRHH', '303', '8888-3333', 3, 1)",
                    @"INSERT INTO Usuario (id_empleado, correo, contrasenna) VALUES
                      (1, 'laura.gomez@empresa.com', '1234'),
                      ( 2,'carlos.morales@empresa.com', 'abcd'),
                      ( 3,'ana.perez@empresa.com', 'admin')"
                };

                foreach (var cmdText in comandos)
                {
                    using var cmd = new SqlCommand(cmdText, connection);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        [Test]
        public void ValidarLogin_CredencialesValidas_RetornaUsuario()
        {
            // Arrange: credenciales que deben existir en la base de datos
            string correo = "laura.gomez@empresa.com";
            string contrasenna = "1234";

            var configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            var usuarioData = new UsuarioData(configurationMock);

            // Act
            var usuario = usuarioData.ValidarLogin(correo, contrasenna);

            // Assert
            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario.Email, Is.EqualTo(correo));
            Assert.That(usuario.Empleado, Is.Not.Null);
            Assert.That(usuario.Empleado.Departamento, Is.Not.Null);
            Assert.That(usuario.Empleado.Rol, Is.Not.Null);
        }

        [Test]
        public void ValidarLogin_CredencialesInvalidas_RetornaNull()
        {
            // Arrange
            string correo = "noexiste@empresa.com";
            string contrasenna = "claveIncorrecta";

            var configurationMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", connectionString }
                }).Build();

            var usuarioData = new UsuarioData(configurationMock);

            // Act
            var usuario = usuarioData.ValidarLogin(correo, contrasenna);

            // Assert
            Assert.That(usuario, Is.Null);
        }
    }
}