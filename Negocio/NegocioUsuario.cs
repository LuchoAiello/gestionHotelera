using Dao;
using Entidades;
using System.Data;

namespace Negocio
{
    public class NegocioUsuario
    {
        DaoUser dao = new DaoUser();
        public DataTable GetUser()
        {
            return dao.GetUser();
        }

        public void CrearUsuario(Usuario user)
        {
            dao.CrearUsuario(user);
        }

        public void ModificarUsuario(Usuario user)
        {
           dao.ModificarUsuario(user);
        }

        public void EliminarUsuario(Usuario user)
        {
            dao.EliminarUsuario(user);
        }
    }
}
