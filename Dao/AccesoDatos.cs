using Entidades;
using System;
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
            //string rutaGestionHotelera =
            //    "Data Source=localhost\\sqlexpress;" + "Initial Catalog=GestionHotelera;" + "Integrated Security=True;" + "Encrypt=True;" + "TrustServerCertificate=True";
            
            //CADENA DE CONEXION PARA CAMI
            string rutaGestionHotelera =
                "Data Source=CAMI;Initial Catalog=GestionHotelera;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

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

        public loggedUser VerifyLogin(string usuario, string password)
        {
            loggedUser user = null;

            string query = "SELECT Nombre, Rol FROM Usuarios WHERE Nombre = @usuario AND Contrasenia = @password";

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
                            user = new loggedUser
                            {
                                Nombre = reader["Nombre"].ToString(),
                                Rol = reader["Rol"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }

        public DataTable SPHistorialReservasFilter(string nombreSP, string numeroHabitacion, string fechaDesde, string fechaHasta)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NumeroHabitacion",
                    string.IsNullOrWhiteSpace(numeroHabitacion) ? (object)DBNull.Value : int.Parse(numeroHabitacion));

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


    }
}
