using Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class NegocioHabitaciones
    {
        DaoHabitaciones dao = new DaoHabitaciones();
        public DataTable GetAll()
        {
            var repo = new Dao.DaoHabitaciones();
            return repo.GetAll();
        }
        public DataTable GetByFilter(string filtro)
        {
            var repo = new Dao.DaoHabitaciones();
            var listaFiltrada = repo.GetByFilter(filtro);
            return listaFiltrada;
        }
        public void CrearHabitacion(Habitaciones habitacion)
        {
            dao.CrearHabitacion(habitacion);
        }

        public void ModificarHabitacion(Habitaciones habitacion)
        {
            dao.ModificarHabitacion(habitacion);
        }

        public DataTable FiltarHabitacionesPorFecha(string fechaLlegada, string fechaSalida)
        {
            var repo = new Dao.DaoHabitaciones();
            return repo.FiltarHabitacionesPorFecha(fechaLlegada, fechaSalida);
        }
    }
}
