using Dao;
using Entidades;
using System.Data;
using static Entidades.Reserva;

namespace Negocio
{
    public class NegocioReserva
    {
        DaoReserva dao = new DaoReserva();
        public DataTable GetReservas()
        {
            return dao.GetReservas();
        }

        public bool EliminarReserva(int idReserva)
        {
            return dao.EliminarReserva(idReserva);
        }

        public DataTable ObtenerDetallesPorReserva(int idReserva)
        {
            return dao.ObtenerDetallesPorReserva(idReserva);
        }
        public bool GuardarReserva(ReservaEnProceso reserva)
        {
            return dao.GuardarReserva(reserva);
        }

    }

}
