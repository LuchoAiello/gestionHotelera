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

        public void CrearServicio(Servicios servicio)
        {
            dao.CrearServicio(servicio);
        }

        public void ModificarServicio(Servicios servicio)
        {
            dao.ModificarServicio(servicio);
        }

        public void EliminarServicio(Servicios servicio)
        {
            dao.EliminarServicio(servicio);
        }
    }
}
