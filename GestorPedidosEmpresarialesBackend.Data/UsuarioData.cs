using GestorPedidosEmpresarialesBackend.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Data
{
    public class UsuarioData
    {
        private readonly string connectionString;

        public UsuarioData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Método para validar el inicio de sesión y traer los datos completos del empleado,
        /// su rol y departamento, asegurando que ninguno esté eliminado.
        /// </summary>
        public Usuario ValidarLogin(string correo, string contrasenna)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        u.id_usuario,
                        u.correo,
                        u.contrasenna,

                        e.id_empleado,
                        e.nombre_empleado,
                        e.apellidos_empleado,
                        e.puesto,
                        e.extension,
                        e.telefono_trabajo,

                        d.id_departamento,
                        d.nombre_departamento,

                        r.id_rol,
                        r.nombre_rol

                    FROM Usuario u
                    INNER JOIN Empleado e ON u.id_empleado = e.id_empleado
                    INNER JOIN Departamento d ON e.id_departamento = d.id_departamento
                    INNER JOIN Rol r ON e.id_rol = r.id_rol
                    WHERE 
                        u.correo = @correo AND u.contrasenna = @contrasenna
                        AND u.id_empleado IS NOT NULL
                        AND e.eliminado = 0
                        AND d.eliminado = 0
                        AND r.eliminado = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@correo", correo);
                    command.Parameters.AddWithValue("@contrasenna", contrasenna);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                Email = reader.GetString(reader.GetOrdinal("correo")).Trim(),
                                Contrasenna = reader.GetString(reader.GetOrdinal("contrasenna")).Trim(),
                                Empleado = new Empleado
                                {
                                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
                                    NombreEmpleado = reader.GetString(reader.GetOrdinal("nombre_empleado")),
                                    ApellidosEmpleado = reader.GetString(reader.GetOrdinal("apellidos_empleado")),
                                    Puesto = reader.IsDBNull(reader.GetOrdinal("puesto")) ? null : reader.GetString(reader.GetOrdinal("puesto")),
                                    Extension = reader.IsDBNull(reader.GetOrdinal("extension")) ? null : reader.GetString(reader.GetOrdinal("extension")),
                                    TelefonoTrabajo = reader.IsDBNull(reader.GetOrdinal("telefono_trabajo")) ? null : reader.GetString(reader.GetOrdinal("telefono_trabajo")),
                                    Departamento = new Departamento
                                    {
                                        IdDepartamento = reader.GetInt32(reader.GetOrdinal("id_departamento")),
                                        NombreDepartamento = reader.GetString(reader.GetOrdinal("nombre_departamento"))
                                    },
                                    Rol = new Rol
                                    {
                                        IdRol = reader.GetInt32(reader.GetOrdinal("id_rol")),
                                        NombreRol = reader.GetString(reader.GetOrdinal("nombre_rol")),
                                        Eliminado = false // Asumiendo que el valor predeterminado es 'false' ya que no se proporciona en la consulta
                                    }
                                 
                                }
                            };
                        }
                    }
                }
            }

            return null; // No se encontró usuario válido
        }
    }
}
