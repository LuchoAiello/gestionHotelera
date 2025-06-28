using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Dao;
using System.Data.SqlClient;


namespace Dao
{
    public class DaoHuespedes
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametroDNI(SqlCommand comando, string dni)
        {
            comando.Parameters.AddWithValue("@DNIhuesped", dni);
        }

        public DataTable GetHuespedes()
        {
            string query = "SELECT * FROM Huespedes WHERE Estado = 1";
            DataTable tabla = ds.ObtenerTabla("Huespedes", query);
            return tabla;
        }

        public bool CrearHuesped(Huespedes huesped)
        {
            return ds.GestionarHuespedConSP(huesped, "CREATE");
        }

        public bool ModificarHuesped(Huespedes huesped)
        {
            return ds.GestionarHuespedConSP(huesped, "UPDATE");
        }

        public bool EliminarHuesped(Huespedes huesped)
        {
            return ds.GestionarHuespedConSP(huesped, "DELETE");
        }

        public DataTable FiltrarHuespedPorDocumento(string Documento)
        {
            return ds.SPFiltroHuespedPorDocumento("sp_FiltroHuespedPorDocumento", Documento);
        }
    }
}
