using Dao;
using Entidades;
using System.Data;

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
    }
}
