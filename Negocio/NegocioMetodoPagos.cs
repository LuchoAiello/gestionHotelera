using Dao;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioMetodoPagos
    {
        DaoMetodoPagos dao = new DaoMetodoPagos();
        public DataTable GetMetodoPagos()
        {
            return dao.GetMetodoPagos();
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
