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
    namespace Negocio
    {
        public class NegocioHuespedes
        {
            DaoHuespedes dao = new DaoHuespedes();

            public DataTable GetHuespedes()
            {
                return dao.GetHuespedes();
            }

            public void CrearHuesped(Huespedes huesped)
            {
                dao.CrearHuesped(huesped);
            }

            public void ModificarHuesped(Huespedes huesped)
            {
                dao.ModificarHuesped(huesped);
            }

            public void EliminarHuesped(Huespedes huesped)
            {
                dao.EliminarHuesped(huesped);
            }

            public DataTable FiltrarHuespedPorDocumento(string Documento)
            {
                return dao.FiltrarHuespedPorDocumento(Documento);
            }
        }
    }
}
