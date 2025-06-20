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
    public class NegocioServicios
    {
        DaoServicios dao = new DaoServicios();
        public DataTable GetServicios()
        {
            return dao.GetServicios();
        }

        public void ModificarServicio(Servicios servicios)
        {
            dao.ModificarServicio(servicios);
        }
        public void CrearServicio(Servicios servicios)
        {
            dao.CrearServicio(servicios);
        }
    }
}
