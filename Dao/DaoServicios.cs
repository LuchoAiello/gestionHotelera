using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoServicios
    {
        AccesoDatos ds = new AccesoDatos();
        public DataTable GetServicios()
        {
            string query = "SELECT * FROM ServiciosAdicionales WHERE Estado = 1";
            DataTable tabla = ds.ObtenerTabla("ServiciosAdicionales", query);
            return tabla;
        }

        public bool CrearServicio(Servicios servicio)
        {
            return ds.GestionarServicioAdicionalConSP(servicio, "CREATE");
        }

        public bool ModificarServicio(Servicios servicio)
        {
            return ds.GestionarServicioAdicionalConSP(servicio, "UPDATE");
        }

        public bool EliminarServicio(Servicios servicio)
        {
            return ds.GestionarServicioAdicionalConSP(servicio, "DELETE");
        }

    }
}
