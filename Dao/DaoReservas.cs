using Entidades;
using System;
using System.Collections;
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

        public int ObtenerIdReservaDesdeDetalle(int idDetalleReserva)
        {
            string consulta = "SELECT Id_reserva FROM Vista_DetallesReserva WHERE Id_detalleReserva = @Id_detalleReserva";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_detalleReserva", idDetalleReserva)
            };

            object resultado = ds.EjecutarScalarConParametros(consulta, parametros);

            if (resultado != null && resultado != DBNull.Value)
                return Convert.ToInt32(resultado);

            return -1;
        }

        public DataTable ObtenerReservaPorId(int IdReserva)
        {
            string consulta = "SELECT * FROM Vista_ReservasActualesYFuturas WHERE Id_Reserva = @Id_reserva";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_reserva", IdReserva)
            };
            parametros[0] = new SqlParameter("@Id_reserva", IdReserva);

            return ds.ObtenerTablaReservas("Vista_ReservasActualesYFuturas", consulta, parametros);
        }

        public DataTable ObtenerHistorialReservaPorId(int IdReserva)
        {
            string consulta = "SELECT * FROM Vista_ReservasHistorial WHERE Id_Reserva = @Id_reserva";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_reserva", IdReserva)
            };
            parametros[0] = new SqlParameter("@Id_reserva", IdReserva);

            return ds.ObtenerTablaReservas("Vista_ReservasHistorial", consulta, parametros);
        }

        public bool GuardarReserva(ReservaEnProceso reserva)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                 new SqlParameter("@Id_huesped", reserva.IdHuesped),
                 new SqlParameter("@Id_metodoPago", reserva.IdMetodoPago),
                 new SqlParameter("@FechaLlegada", reserva.FechaLlegada),
                 new SqlParameter("@FechaSalida", reserva.FechaSalida),
                 new SqlParameter("@CantidadHuespedes", reserva.CantidadHuespedes),
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

            foreach (int id in reserva.IdHabitaciones)
            {
                dt.Rows.Add(id);
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

        public void RegistrarCheckIn(int idDetalleReserva)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_detalleReserva", idDetalleReserva)
            };

            ds.EjecutarProcedimientoConParametros("SP_AsignarCheckIn", parametros);
        }

        public void RegistrarCheckOut(int idDetalleReserva)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_detalleReserva", idDetalleReserva)
            };

            ds.EjecutarProcedimientoConParametros("SP_AsignarCheckOut", parametros);
        }

    }
}
