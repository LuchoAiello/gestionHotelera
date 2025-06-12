using System;
using System.Web;
using Dao;
using Entidades;

namespace Negocio
{
    public class NegocioLogin
    {
        DaoLogin dao = new DaoLogin();

        public loggedUser Login(string user, string password)
        {
            return dao.Login(user, password);
        }
    }
}
