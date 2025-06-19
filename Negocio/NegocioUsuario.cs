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
    public class NegocioUsuario
    {
        DaoUser dao = new DaoUser();
        public DataTable GetUser()
        {
            return dao.GetUser();
        }

        public void ModificarUsuario(Usuario user)
        {
           dao.ModificarUsuario(user);
        }

        public void CrearUsuario(Usuario user)
        {
            dao.CrearUsuario(user);
        }
    }
}
