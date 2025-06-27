using Dao;
using Entidades;
using System.Data;

namespace Negocio
{
    public class NegocioServicios
    {
        DaoServicios dao = new DaoServicios();
        public DataTable GetServicios()
        {
            return dao.GetServicios();
        }

        public DataTable GetServiciosActivos()
        {
            return dao.GetServiciosActivos();
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
