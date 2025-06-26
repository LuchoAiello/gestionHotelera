using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoReserva
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametros(SqlCommand comando, Reserva r)
        {
            comando.Parameters.AddWithValue("@Id_reserva", r.IdReserva);
            comando.Parameters.AddWithValue("@Nombre", r.NombreCompleto);
            comando.Parameters.AddWithValue("@Documento", r.Documento);
            comando.Parameters.AddWithValue("@Email", r.Email);
            comando.Parameters.AddWithValue("@Telefono", r.Telefono);
            comando.Parameters.AddWithValue("@FechaReserva", r.FechaReserva);
            comando.Parameters.AddWithValue("@CantidadHuespedes", r.CantidadHuespedes);
            comando.Parameters.AddWithValue("@PrecioFinal", r.PrecioFinal);
        }

        public DataTable GetReservas()
        {
            string consulta = "SELECT * FROM Vista_Reservas";
            return ds.ObtenerTabla("Reservas", consulta);
        }
        public bool ModificarReserva(Reserva r)
        {
            SqlCommand comando = new SqlCommand();
            comando.Parameters.AddWithValue("@Id_reserva", r.IdReserva);
            comando.Parameters.AddWithValue("@CantidadHuespedes", r.CantidadHuespedes);

            string consulta = @"
        UPDATE Reservas SET 
            CantidadHuespedes = @CantidadHuespedes
        WHERE Id_reserva = @Id_reserva;
    ";

            return ds.EjecutarConsulta(consulta, comando);
        }

        public DataTable ObtenerDetallesPorReserva(int idReserva)
        {
            string query = "SELECT * FROM Vista_DetallesReserva WHERE Id_reserva = @Id_reserva";

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@Id_reserva", idReserva);

            return ds.ObtenerTablaReservas("Vista_DetallesReserva", query, parametros);
        }

    }
}
