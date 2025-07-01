using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class EmpleadoData
    {
        private readonly string _connectionString;

        public EmpleadoData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Empleado> GetAll()
        {
            var empleados = new List<Empleado>();
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT e.id_empleado, e.nombre_empleado, e.apellidos_empleado, e.puesto, e.extension, e.telefono_trabajo, e.eliminado,
                           d.id_departamento, d.nombre_departamento, r.id_rol, r.nombre_rol
                    FROM Empleado e
                    JOIN Departamento d ON e.id_departamento = d.id_departamento
                    JOIN Rol r ON e.id_rol = r.id_rol
                    WHERE e.eliminado = 0 AND d.eliminado = 0 AND r.eliminado = 0";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        empleados.Add(MapToEmpleado(reader));
                    }
                }
            }
            return empleados;
        }

        public Empleado GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT e.id_empleado, e.nombre_empleado, e.apellidos_empleado, e.puesto, e.extension, e.telefono_trabajo, e.eliminado,
                           d.id_departamento, d.nombre_departamento, r.id_rol, r.nombre_rol
                    FROM Empleado e
                    JOIN Departamento d ON e.id_departamento = d.id_departamento
                    JOIN Rol r ON e.id_rol = r.id_rol
                    WHERE e.id_empleado = @id AND e.eliminado = 0";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToEmpleado(reader);
                    }
                }
            }
            return null;
        }

        public Empleado Create(Empleado empleado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO Empleado (nombre_empleado, apellidos_empleado, puesto, extension, telefono_trabajo, id_departamento, id_rol, eliminado)
                    VALUES (@nombre, @apellidos, @puesto, @extension, @telefono, @id_departamento, @id_rol, 0);
                    SELECT SCOPE_IDENTITY();";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", empleado.NombreEmpleado);
                command.Parameters.AddWithValue("@apellidos", empleado.ApellidosEmpleado);
                command.Parameters.AddWithValue("@puesto", empleado.Puesto ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@extension", empleado.Extension ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@telefono", empleado.TelefonoTrabajo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id_departamento", empleado.Departamento.IdDepartamento);
                command.Parameters.AddWithValue("@id_rol", empleado.Rol.IdRol);
                connection.Open();
                empleado.IdEmpleado = Convert.ToInt32(command.ExecuteScalar());
            }
            return empleado;
        }

        public void Update(Empleado empleado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE Empleado
                    SET nombre_empleado = @nombre,
                        apellidos_empleado = @apellidos,
                        puesto = @puesto,
                        extension = @extension,
                        telefono_trabajo = @telefono,
                        id_departamento = @id_departamento,
                        id_rol = @id_rol
                    WHERE id_empleado = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", empleado.IdEmpleado);
                command.Parameters.AddWithValue("@nombre", empleado.NombreEmpleado);
                command.Parameters.AddWithValue("@apellidos", empleado.ApellidosEmpleado);
                command.Parameters.AddWithValue("@puesto", empleado.Puesto ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@extension", empleado.Extension ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@telefono", empleado.TelefonoTrabajo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id_departamento", empleado.Departamento.IdDepartamento);
                command.Parameters.AddWithValue("@id_rol", empleado.Rol.IdRol);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Empleado SET eliminado = 1 WHERE id_empleado = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private Empleado MapToEmpleado(SqlDataReader reader)
        {
            return new Empleado
            {
                IdEmpleado = (int)reader["id_empleado"],
                NombreEmpleado = (string)reader["nombre_empleado"],
                ApellidosEmpleado = (string)reader["apellidos_empleado"],
                Puesto = reader["puesto"] as string,
                Extension = reader["extension"] as string,
                TelefonoTrabajo = reader["telefono_trabajo"] as string,
                Eliminado = (bool)reader["eliminado"],
                Departamento = new Departamento
                {
                    IdDepartamento = (int)reader["id_departamento"],
                    NombreDepartamento = (string)reader["nombre_departamento"]
                },
                Rol = new Rol
                {
                    IdRol = (int)reader["id_rol"],
                    NombreRol = (string)reader["nombre_rol"]
                }
            };
        }
    }
}