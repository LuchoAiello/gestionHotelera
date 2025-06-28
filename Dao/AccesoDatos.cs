using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    class AccesoDatos
    {
        public AccesoDatos() { }

        public SqlConnection ObtenerConexion()
        {
            //CADENA DE CONEXION PARA LUCHO
            string rutaGestionHotelera =
              "Data Source=localhost\\sqlexpress;" + "Initial Catalog=GestionHotelera;" + "Integrated Security=True;" + "Encrypt=True;" + "TrustServerCertificate=True";

            //CADENA DE CONEXION PARA CAMI
            //string rutaGestionHotelera =
             //   "Data Source=CAMI;Initial Catalog=GestionHotelera;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                SqlConnection cn = new SqlConnection(rutaGestionHotelera);
                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                return null;
            }
        }

        private SqlDataAdapter ObtenerAdaptador(string consultaSql, SqlConnection cn)
        {
            SqlDataAdapter adaptador;
            try
            {
                adaptador = new SqlDataAdapter(consultaSql, cn);
                return adaptador;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                return null;
            }
        }

        public DataTable ObtenerTabla(string NombreTabla, string Sql)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection Conexion = ObtenerConexion();
                SqlDataAdapter adp = ObtenerAdaptador(Sql, Conexion);
                adp.Fill(ds, NombreTabla);
                Conexion.Close();
                return ds.Tables[NombreTabla];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la tabla '{NombreTabla}' con la consulta '{Sql}': {ex.Message}");
            }
        }

        public DataTable ObtenerTablaReservas(string nombreTabla, string sql, SqlParameter[] parametros)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection conexion = ObtenerConexion();
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddRange(parametros);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds, nombreTabla);
                conexion.Close();
                return ds.Tables[nombreTabla];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la tabla '{nombreTabla}' con la consulta '{sql}': {ex.Message}");
            }
        }

        public bool EjecutarConsulta(string consulta, SqlCommand comando)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = this.ObtenerConexion();
                comando.Connection = conexion;
                comando.CommandText = consulta;

                return comando.ExecuteNonQuery() > 0;
            }
            catch (Exception conexionerror)
            {
                Console.WriteLine(conexionerror.Message);
                throw new Exception("Error al ejecutar consulta", conexionerror);
            }
            finally
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public LoggedUser VerifyLogin(string usuario, string password)
        {
            LoggedUser user = null;

            string query = "SELECT Nombre, Rol FROM Usuarios WHERE Nombre = @usuario AND Contrasenia = @password AND Estado != 0";

            using (SqlConnection conexion = ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new LoggedUser
                            {
                                Nombre = reader["Nombre"].ToString(),
                                Rol = reader["Rol"].ToString(),
                            };
                        }
                    }
                }
            }

            return user;
        }

        public DataTable SPHistorialReservasFilter(string nombreSP, string filtro, string fechaDesde, string fechaHasta)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@filtro",
                    string.IsNullOrWhiteSpace(filtro) ? (object)DBNull.Value : filtro);

                cmd.Parameters.AddWithValue("@FechaDesde",
                    string.IsNullOrWhiteSpace(fechaDesde) ? (object)DBNull.Value : DateTime.Parse(fechaDesde));

                cmd.Parameters.AddWithValue("@FechaHasta",
                    string.IsNullOrWhiteSpace(fechaHasta) ? (object)DBNull.Value : DateTime.Parse(fechaHasta));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                return tabla;
            }
        }

        public DataTable SPHabitacionesDisponibles(string nombreSP, string fechaLlegada, string fechaSalida)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaLlegada",
                    string.IsNullOrWhiteSpace(fechaLlegada) ? (object)DBNull.Value : DateTime.Parse(fechaLlegada));

                cmd.Parameters.AddWithValue("@FechaSalida",
                    string.IsNullOrWhiteSpace(fechaSalida) ? (object)DBNull.Value : DateTime.Parse(fechaSalida));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                return tabla;
            }
        }

        public DataTable SPFiltrarHabitaciones(string nombreSP, string filtro)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@filtro",
                    string.IsNullOrWhiteSpace(filtro) ? (object)DBNull.Value : filtro);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                return tabla;
            }
        }

        public DataTable SPFiltrarHistorialReservas(string nombreSP, string filtro)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@filtro",
                    string.IsNullOrWhiteSpace(filtro) ? (object)DBNull.Value : filtro);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                return tabla;
            }
        }

        public bool GestionarUsuarioConSP(Usuario usuario, string accion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_GestionarUsuario", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Accion", accion);

                if (accion == "UPDATE" || accion == "DELETE")
                    comando.Parameters.AddWithValue("@Id_usuario", usuario.IdUsuario);

                if (accion != "DELETE")
                {
                    comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    comando.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                    comando.Parameters.AddWithValue("@Rol", usuario.Rol);
                }

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool GestionarMetodoPagoConSP(MetodoPago metodoPago, string accion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_GestionarMetodoPago", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Accion", accion);

                if (accion == "UPDATE" || accion == "DELETE")
                    comando.Parameters.AddWithValue("@Id_metodoPago", metodoPago.IdMetodoPago);

                if (accion != "DELETE")
                    comando.Parameters.AddWithValue("@Nombre", metodoPago.Nombre);

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool GestionarServicioAdicionalConSP(Servicios servicio, string accion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_GestionarServicioAdicional", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Accion", accion);

                if (accion == "UPDATE" || accion == "DELETE")
                    comando.Parameters.AddWithValue("@Id_servicioAdicional", servicio.IdServicio);

                if (accion != "DELETE")
                {
                    comando.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                    comando.Parameters.AddWithValue("@Precio", servicio.Precio);
                }

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool GestionarHuespedConSP(Huespedes huesped, string accion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_GestionarHuesped", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Accion", accion);

                if (accion == "UPDATE" || accion == "DELETE")
                    comando.Parameters.AddWithValue("@Id_huesped", huesped.IdHuesped);

                if (accion != "DELETE")
                {
                    comando.Parameters.AddWithValue("@Nombre", huesped.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", huesped.Apellido);
                    comando.Parameters.AddWithValue("@Documento", huesped.Documento);
                    comando.Parameters.AddWithValue("@TipoDocumento", huesped.TipoDocumento);
                    comando.Parameters.AddWithValue("@Email", huesped.Email ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", huesped.Telefono);
                    comando.Parameters.AddWithValue("@FechaNacimiento", huesped.FechaNacimiento);
                }

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool GestionarHabitacionConSP(Habitaciones habitacion, string accion)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_GestionarHabitacion", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Accion", accion);

                if (accion == "UPDATE" || accion == "DELETE")
                    comando.Parameters.AddWithValue("@Id_habitacion", habitacion.Id_habitacion);

                if (accion != "DELETE")
                {
                    comando.Parameters.AddWithValue("@NumeroHabitacion", habitacion.NumeroHabitacion);
                    comando.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                    comando.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
                    comando.Parameters.AddWithValue("@Estado", habitacion.Estado);
                    comando.Parameters.AddWithValue("@Precio", habitacion.Precio);
                    comando.Parameters.AddWithValue("@Descripcion", (object)habitacion.Descripcion ?? DBNull.Value);
                }

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }


        public DataTable SPFiltroHuespedPorDocumento(string nombreSP, string documento)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Documento",
                    string.IsNullOrWhiteSpace(documento) ? (object)DBNull.Value : documento);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                return tabla;
            }
        }

        public int EjecutarProcedimientoConParametros(string nombreSP, SqlParameter[] parametros)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametros);
                return cmd.ExecuteNonQuery(); 
            }
        }
        public object EjecutarScalarConParametros(string consulta, SqlParameter[] parametros)
        {
            object resultado = null;

            using (SqlConnection conexion = ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddRange(parametros);
                    
                    resultado = comando.ExecuteScalar();
                }
            }

            return resultado;
        }
    }
}
