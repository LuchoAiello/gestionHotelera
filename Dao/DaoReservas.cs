using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static Entidades.Reserva;

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
            comando.Parameters.AddWithValue("@Estado", r.Estado);
        }

        public DataTable GetReservas()
        {
            string consulta = "SELECT * FROM Vista_Reservas";
            return ds.ObtenerTabla("Reservas", consulta);
        }
        public bool EliminarReserva(int idReserva)
        {
            SqlCommand comando = new SqlCommand();
            comando.Parameters.AddWithValue("@Id_reserva", idReserva);

            string consulta = @"
             DELETE FROM Reservas
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

        public bool GuardarReserva(ReservaEnProceso reserva)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdHuesped", reserva.IdHuesped),
                new SqlParameter("@IdMetodoPago", reserva.IdMetodoPago),
                new SqlParameter("@FechaPago", DateTime.Now),
                new SqlParameter("@MontoTotal", reserva.PrecioFinal),
                new SqlParameter("@FechaReserva", reserva.FechaReserva),
                new SqlParameter("@FechaLlegada", reserva.CheckIn),
                new SqlParameter("@FechaSalida", reserva.CheckOut),
                new SqlParameter("@CantidadHuespedes", reserva.CantidadHuespedes),
                new SqlParameter("@NroTarjeta", reserva.NroTarjeta),
                new SqlParameter("@VencimientoTarjeta", reserva.VenTarjeta),
                new SqlParameter("@Habitaciones", ConvertirListaHabitaciones(reserva.IdHabitaciones)),
                new SqlParameter("@Servicios", ConvertirListaServicios(reserva.ServiciosAdicionales))
            };

            int filasAfectadas = ds.EjecutarProcedimientoConParametros("sp_RegistrarReservaCompleta", parametros);
            return filasAfectadas > 0;
        }

        // Métodos para convertir las listas a DataTable (igual que tenías)
        private DataTable ConvertirListaHabitaciones(List<int> ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            foreach (int id in ids)
                table.Rows.Add(id);
            return table;
        }

        private DataTable ConvertirListaServicios(List<int> ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            foreach (int id in ids)
                table.Rows.Add(id);
            return table;
        }
    }
}
