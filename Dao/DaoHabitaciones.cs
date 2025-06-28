using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoHabitaciones
    {
        private AccesoDatos datos = new AccesoDatos();

        public DataTable GetAll()
        {
            string sql = "SELECT * FROM Habitaciones";
            return datos.ObtenerTabla("Habitaciones", sql);
        }

        public DataTable GetByFilter(string filtro)
        {
            return datos.SPFiltrarHabitaciones("sp_FiltrarHabitaciones", filtro);
        }

        public bool CrearHabitacion(Habitaciones habitacion)
        {
            return datos.GestionarHabitacionConSP(habitacion, "CREATE");
        }

        public bool ModificarHabitacion(Habitaciones habitacion)
        {
            return datos.GestionarHabitacionConSP(habitacion, "UPDATE");
        }

        public bool EliminarHabitacion(Habitaciones habitacion)
        {
            return datos.GestionarHabitacionConSP(habitacion, "DELETE");
        }

        public DataTable FiltarHabitacionesPorFecha(string fechaLlegada, string fechaSalida)
        {
            return datos.SPHabitacionesDisponibles("sp_HabitacionesDisponibles", fechaLlegada, fechaSalida);
        }
    }
}
