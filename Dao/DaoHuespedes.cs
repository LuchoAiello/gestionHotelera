using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Dao;
using System.Data.SqlClient;


namespace Dao
{
    public class DaoHuespedes
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametrosHuesped(SqlCommand Comando, Huespedes huesped)
        {
            Comando.Parameters.AddWithValue("@Id_huesped", huesped.IdHuesped);
            Comando.Parameters.AddWithValue("@Nombre", huesped.Nombre);
            Comando.Parameters.AddWithValue("@Apellido", huesped.Apellido);
            Comando.Parameters.AddWithValue("@Documento", huesped.Documento);
            Comando.Parameters.AddWithValue("@TipoDocumento", huesped.TipoDocumento);
            Comando.Parameters.AddWithValue("@Email", huesped.Email);
            Comando.Parameters.AddWithValue("@Telefono", huesped.Telefono);
            Comando.Parameters.AddWithValue("@FechaNacimiento", huesped.FechaNacimiento);
            Comando.Parameters.AddWithValue("@Estado", huesped.Estado);
        }


        public DataTable GetHuespedes()
        {
            string query = "SELECT * FROM Huespedes";
            DataTable tabla = ds.ObtenerTabla("Huespedes", query);
            return tabla;
        }

        public bool ModificarHuesped(Huespedes huesped)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosHuesped(comando, huesped);

            string consulta = @"UPDATE Huespedes SET 
                            Nombre = @Nombre,
                            Apellido = @Apellido,
                            Documento = @Documento,
                            TipoDocumento = @TipoDocumento,
                            Email = @Email,
                            Telefono = @Telefono,
                            FechaNacimiento = @FechaNacimiento,
                            Estado = @Estado
                        WHERE Id_huesped = @Id_huesped";

            return ds.EjecutarConsulta(consulta, comando);
        }


        public bool CrearHuesped(Huespedes huesped)
        {
            SqlCommand comando = new SqlCommand();
            ArmarParametrosHuesped(comando, huesped);

            string consulta = @"INSERT INTO Huespedes 
                        (Nombre, Apellido, Documento, TipoDocumento, Email, Telefono, FechaNacimiento)
                        VALUES 
                        (@Nombre, @Apellido, @Documento, @TipoDocumento, @Email, @Telefono, @FechaNacimiento)";

            return ds.EjecutarConsulta(consulta, comando);
        }
    }
}
