using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Dao
{
    class AccesoDatos
    {
        public AccesoDatos() { }
        
        public SqlConnection ObtenerConexion()
        {
            string rutaGestionHotelera =
                "Data Source=localhost\\sqlexpress;Initial Catalog=GestionHotelera;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            
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

            string query = "SELECT Nombre, Rol FROM Usuarios WHERE Nombre = @usuario AND Contraseña = @password";

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

        private bool HasColumn (SqlDataReader reader, string columnName)
        {
            return Enumerable.Range(0, reader.FieldCount)
                             .Select(reader.GetName)
                             .Any(n => n.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool validarDniPaciente(string consulta)
        {
            bool validar = false;

            DataSet ds = new DataSet();
            SqlConnection conexion = ObtenerConexion();
            SqlDataAdapter adp = ObtenerAdaptador(consulta, conexion);

            adp.Fill(ds);
            conexion.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
               validar = true;
            }

            return validar;
        }

        public bool BajaPaciente(string consulta)
        {
            bool dadoDeBaja = false;

            using (SqlCommand comando = new SqlCommand())
            {
                try
                {
                    SqlConnection conexion = ObtenerConexion();
                    comando.Connection = conexion;
                    comando.CommandText = consulta;

                    int filasAfectadas = comando.ExecuteNonQuery();
                    dadoDeBaja = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al dar de baja el médico en la base de datos", ex);
                }
            }


            return dadoDeBaja;
        }

        public DataTable ObtenerTablaConComando(SqlCommand comando)
        {
            DataSet ds = new DataSet();

            SqlConnection Conexion = ObtenerConexion();

            comando.Connection = Conexion;

            SqlDataAdapter adp = new SqlDataAdapter(comando);

            adp.Fill(ds);

            Conexion.Close();

            return ds.Tables[0]; 
        }


    }
}
