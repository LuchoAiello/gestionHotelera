using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoMetodoPagos
    {
        AccesoDatos ds = new AccesoDatos();

        public DataTable GetMetodoPagos()
        {
            string query = "SELECT * FROM MetodoPago WHERE Estado = 1";
            DataTable tabla = ds.ObtenerTabla("MetodoPago", query);
            return tabla;
        }

        public bool CrearMetodoPago(MetodoPago pago)
        {
            return ds.GestionarMetodoPagoConSP(pago, "CREATE");
        }

        public bool ModificarMetodoPago(MetodoPago pago)
        {
            return ds.GestionarMetodoPagoConSP(pago, "UPDATE");
        }

        public bool EliminarMetodoPago(MetodoPago pago)
        {
            return ds.GestionarMetodoPagoConSP(pago, "DELETE");
        }

    }
}
