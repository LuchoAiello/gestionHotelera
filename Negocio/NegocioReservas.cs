using Dao;
using Entidades;
using System.Data;
using System.Data.SqlClient;
using static Entidades.Reserva;

namespace Negocio
{
    public class NegocioReserva
    {
        DaoReserva dao = new DaoReserva();
        public DataTable GetReservasActualesYFuturas()
        {
            return dao.GetReservasActualesYFuturas();
        }

        public DataTable GetHistorialDeReservas()
        {
            return dao.GetHistorialDeReservas();
        }

        public DataTable ObtenerReservaPorId(int idReserva)
        {
            return dao.ObtenerReservaPorId(idReserva);
        }

        public DataTable ObtenerHistorialReservaPorId(int idReserva)
        {
            return dao.ObtenerHistorialReservaPorId(idReserva);
        }
        public bool EliminarReserva(int idReserva)
        {
            return dao.EliminarReserva(idReserva);
        }

        public int ObtenerIdReservaDesdeDetalle(int idReserva)
        {
            return dao.ObtenerIdReservaDesdeDetalle(idReserva);
        }

        public DataTable ObtenerDetallesPorReserva(int idReserva)
        {
            return dao.ObtenerDetallesPorReserva(idReserva);
        }
        public bool GuardarReserva(ReservaEnProceso reserva)
        {
            return dao.GuardarReserva(reserva);
        }

        public void RegistrarCheckIn(int idDetalleReserva)
        {
            dao.RegistrarCheckIn(idDetalleReserva);
        }

        public void RegistrarCheckOut(int idDetalleReserva)
        {
            dao.RegistrarCheckOut(idDetalleReserva);
        }
    }
}
