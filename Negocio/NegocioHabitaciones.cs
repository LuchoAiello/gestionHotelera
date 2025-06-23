using Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioHabitaciones
    {

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
        public bool Insert(Entidades.Habitacion h)
        {
            Dao.DaoHabitaciones repo = new Dao.DaoHabitaciones();
            return repo.Insert(h);
        }
        public bool Update(Entidades.Habitacion h)
        {
            Dao.DaoHabitaciones repo = new Dao.DaoHabitaciones();
            return repo.Update(h);
        }
        public bool Delete(int id)
        {
            Dao.DaoHabitaciones repo = new Dao.DaoHabitaciones();
            return repo.Delete(id);
        }
    }
}
