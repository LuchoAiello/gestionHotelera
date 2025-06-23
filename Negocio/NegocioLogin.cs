using Dao;
using Entidades;

namespace Negocio
{
    public class NegocioLogin
    {
        DaoLogin dao = new DaoLogin();

        public LoggedUser Login(string user, string password)
        {
            return dao.Login(user, password);
        }
    }
}
