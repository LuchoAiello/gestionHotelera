using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class DaoHistorialReservas
    {
        AccesoDatos ds = new AccesoDatos();

        public DataTable GetHistorialReserva()
        {
            string query = "SELECT * FROM Vista_HistorialDeReservas";
            DataTable tabla = ds.ObtenerTabla("Vista_HistorialDeReservas", query);
            return tabla;
        }
        public DataTable ObtenerHistorialReservaFiltrado(string numeroHabitacion, string fechaDesde, string fechaHasta)
        {
            return ds.SPHistorialReservasFilter("sp_FiltrarHistorialDeReservas", numeroHabitacion, fechaDesde, fechaHasta);
        }
    }
}
