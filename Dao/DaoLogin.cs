using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class DaoLogin
    {
        AccesoDatos ds = new AccesoDatos();

        public LoggedUser Login(string user, string password)
        {
            return ds.VerifyLogin(user, password);
        }

    }
}
