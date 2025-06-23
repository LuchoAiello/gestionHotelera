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
            string sql = "EXEC SP_ListarHabitaciones";
            return datos.ObtenerTabla("Habitaciones", sql);
        }

        public DataTable GetByFilter(string filtro)
        {
            return datos.SPFiltrarHabitaciones("sp_FiltrarHabitaciones", filtro);
        }

        public bool Insert(Habitacion h)
        {
            string consulta = @"INSERT INTO Habitaciones 
                (NumeroHabitacion, Tipo, Capacidad, Estado, Precio, Descripcion)
                VALUES (@Numero, @Tipo, @Capacidad, @Estado, @Precio, @Descripcion)";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Numero", h.NumeroHabitacion);
            cmd.Parameters.AddWithValue("@Tipo", h.Tipo);
            cmd.Parameters.AddWithValue("@Capacidad", h.Capacidad);
            cmd.Parameters.AddWithValue("@Estado", h.Estado);
            cmd.Parameters.AddWithValue("@Precio", h.Precio);
            cmd.Parameters.AddWithValue("@Descripcion", h.Descripcion);

            return datos.EjecutarConsulta(consulta, cmd);
        }

        public bool Update(Habitacion h)
        {
            string consulta = @"UPDATE Habitaciones SET 
                NumeroHabitacion = @Numero,
                Tipo = @Tipo,
                Capacidad = @Capacidad,
                Estado = @Estado,
                Precio = @Precio,
                Descripcion = @Descripcion
                WHERE Id_habitacion = @Id";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Id", h.Id_habitacion);
            cmd.Parameters.AddWithValue("@Numero", h.NumeroHabitacion);
            cmd.Parameters.AddWithValue("@Tipo", h.Tipo);
            cmd.Parameters.AddWithValue("@Capacidad", h.Capacidad);
            cmd.Parameters.AddWithValue("@Estado", h.Estado);
            cmd.Parameters.AddWithValue("@Precio", h.Precio);
            cmd.Parameters.AddWithValue("@Descripcion", h.Descripcion);

            return datos.EjecutarConsulta(consulta, cmd);
        }

        public bool Delete(int id)
        {
            string consulta = "Update Habitaciones Set Estado = 'Inactiva' WHERE Id_habitacion = @Id";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Id", id);
            return datos.EjecutarConsulta(consulta, cmd);
        }
    }
}
