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
            string query = "SELECT * FROM Usuarios WHERE Estado = 1";
            DataTable tabla = ds.ObtenerTabla("Usuarios", query);
            return tabla;
        }
        public bool CrearUsuario(Usuario user)
        {
            return ds.GestionarUsuarioConSP(user, "CREATE");
        }

        public bool ModificarUsuario(Usuario user)
        {
            return ds.GestionarUsuarioConSP(user, "UPDATE");
        }

        public bool EliminarUsuario(Usuario user)
        {
            return ds.GestionarUsuarioConSP(user, "DELETE");
        }
    }
}
