using Dao;
using Entidades;
using System.Data;

namespace Negocio
{
    public class NegocioMetodoPagos
    {
        DaoMetodoPagos dao = new DaoMetodoPagos();
        public DataTable GetMetodoPagos()
        {
            return dao.GetMetodoPagos();
        }

        public DataTable GetMetodoPagosReserva()
        {
            return dao.GetMetodoPagosReserva();
        }
        public void ModificarMetodoPago(MetodoPago pago)
        {
            dao.ModificarMetodoPago(pago);
        }
        public void CrearMetodoPago(MetodoPago pago)
        {
            dao.CrearMetodoPago(pago);
        }
    }
}
