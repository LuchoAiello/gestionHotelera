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

        public bool Crear(Habitacion h)
        {
            return datos.SPCrearHabitacion("SP_CrearHabitacion", h);
        }

        public bool Update(Habitacion h)
        {
            return datos.SPActualizarHabitaciones("SP_ActualizarHabitacion", h);
        }

        public bool Delete(int id)
        {
            string consulta = "Update Habitaciones Set Estado = 'Inactiva' WHERE Id_habitacion = @Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Id", id);
            return datos.EjecutarConsulta(consulta, cmd);
        }

        public DataTable FiltarHabitacionesPorFecha(string fechaLlegada, string fechaSalida)
        {
            return datos.SPHabitacionesDisponibles("sp_HabitacionesDisponibles", fechaLlegada, fechaSalida);
        }
    }
}
