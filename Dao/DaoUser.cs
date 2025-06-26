using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoUser
    {
        AccesoDatos ds = new AccesoDatos();


        public DataTable GetUser()
        {
            string query = "SELECT * FROM Usuarios";
            DataTable tabla = ds.ObtenerTabla("Usuarios", query);
            return tabla;
        }

        public bool ModificarUsuario(Usuario user)
        {
            return ds.ModificarUsuarioConSP(user);
        }

        public bool CrearUsuario(Usuario user)
        {
            return ds.CrearUsuarioConSP(user);
        }
    }
}
