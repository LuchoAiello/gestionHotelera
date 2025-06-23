using Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HabitacionesService
    {

        public DataTable GetAll()
        {
            var repo = new Dao.HabitacionesRepository();
            return repo.GetAll();
        }
        public DataTable GetByFilter(string filtro)
        {
            var repo = new Dao.HabitacionesRepository();
            var listaFiltrada = repo.GetByFilter(filtro);
            return listaFiltrada;
        }
        public bool Insert(Entidades.Habitacion h)
        {
            Dao.HabitacionesRepository repo = new Dao.HabitacionesRepository();
            return repo.Insert(h);
        }
        public bool Update(Entidades.Habitacion h)
        {
            Dao.HabitacionesRepository repo = new Dao.HabitacionesRepository();
            return repo.Update(h);
        }
        public bool Delete(int id)
        {
            Dao.HabitacionesRepository repo = new Dao.HabitacionesRepository();
            return repo.Delete(id);
        }
    }
}
