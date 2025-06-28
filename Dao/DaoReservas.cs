using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using static Entidades.Reserva;

namespace Dao
{
    public class DaoReserva
    {
        AccesoDatos ds = new AccesoDatos();

        public DataTable GetReservasActualesYFuturas()
        {
            string consulta = "SELECT * FROM Vista_ReservasActualesYFuturas ORDER BY FechaSalida ASC";
            return ds.ObtenerTabla("Reservas", consulta);
        }

        public DataTable GetHistorialDeReservas()
        {
            string consulta = "SELECT * FROM Vista_ReservasHistorial ORDER BY FechaSalida DESC";
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
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                 new SqlParameter("@Id_huesped", reserva.IdHuesped),
                 new SqlParameter("@Id_metodoPago", reserva.IdMetodoPago),
                 new SqlParameter("@FechaLlegada", reserva.CheckIn),
                 new SqlParameter("@FechaSalida", reserva.CheckOut),
                 new SqlParameter("@CantidadHuespedes", reserva.CantidadHuespedes),
                 new SqlParameter("@MontoTotal", reserva.PrecioFinal),
                 new SqlParameter("@NroTarjeta", (object)reserva.NroTarjeta ?? DBNull.Value),
                 new SqlParameter("@VtoTarjeta", ConvertirVencimiento(reserva.VtoTarjeta)),
                 new SqlParameter
            {
                ParameterName = "@HabitacionesDetalle",
                SqlDbType = SqlDbType.Structured,
                TypeName = "TipoHabitacionesDetalle",
                Value = ConvertirHabitacionesDetalle(reserva)
            },
                new SqlParameter
            {
                ParameterName = "@ServiciosDetalle",
                SqlDbType = SqlDbType.Structured,
                TypeName = "TipoServiciosDetalle",
                Value = ConvertirServiciosDetalle(reserva)
            }
                };

                ds.EjecutarProcedimientoConParametros("SP_CrearReservaCompleta", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en DAO.GuardarReserva: " + ex.Message, ex);
            }
        }

        private DataTable ConvertirHabitacionesDetalle(ReservaEnProceso reserva)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_habitacion", typeof(int));
            dt.Columns.Add("PrecioDetalle", typeof(decimal));
            dt.Columns.Add("CheckIn", typeof(DateTime));
            dt.Columns.Add("CheckOut", typeof(DateTime));

            foreach (int id in reserva.IdHabitaciones)
            {
                dt.Rows.Add(id, reserva.PrecioFinal, reserva.CheckIn, reserva.CheckOut);
            }

            return dt;
        }

        private DataTable ConvertirServiciosDetalle(ReservaEnProceso reserva)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_habitacion", typeof(int));
            dt.Columns.Add("Id_servicioAdicional", typeof(int));

            foreach (int idHab in reserva.IdHabitaciones)
            {
                foreach (int idServ in reserva.ServiciosAdicionales)
                {
                    dt.Rows.Add(idHab, idServ);
                }
            }

            return dt;
        }

        private object ConvertirVencimiento(string vencimientoMMYY)
        {
            if (DateTime.TryParseExact(vencimientoMMYY, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime vto))
            {
                return new DateTime(vto.Year, vto.Month, DateTime.DaysInMonth(vto.Year, vto.Month));
            }
            return DBNull.Value;
        }

    }
}
