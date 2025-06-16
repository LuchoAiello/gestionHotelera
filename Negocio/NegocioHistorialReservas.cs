using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao;

namespace Negocio
{
    public class NegocioHistorialReservas
    {
        DaoHistorialReservas dao = new DaoHistorialReservas();

        public DataTable GetHistorialReserva()
        {
            return dao.GetHistorialReserva();
        }
        public DataTable GetFilterHistorialReserva(string numeroHabitacion, string fechaDesde, string fechaHasta)
        {
            return dao.ObtenerHistorialReservaFiltrado(numeroHabitacion, fechaDesde, fechaHasta);
        }
    }
}
