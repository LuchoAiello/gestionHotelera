using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoServicios
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametroServicio(SqlCommand Comando, Servicios servicio)
        {
            Comando.Parameters.AddWithValue("@Id_servicioAdicional", servicio.getIdServicio());
            Comando.Parameters.AddWithValue("@NombreServicio", servicio.getNombre());
            Comando.Parameters.AddWithValue("@Precio", servicio.getPrecio());
            Comando.Parameters.AddWithValue("@Estado", servicio.getEstado());

        }
        public DataTable GetServicios()
        {
            string query = "SELECT * FROM ServiciosAdicionales";
            DataTable tabla = ds.ObtenerTabla("ServiciosAdicionales", query);
            return tabla;
        }

        public bool ModificarServicio(Servicios servicio)
        {
            SqlCommand comando = new SqlCommand();
            this.ArmarParametroServicio(comando, servicio);
            string consulta = @"UPDATE ServiciosAdicionales
                   SET NombreServicio = @NombreServicio, 
                       Precio = @Precio,
                       Estado = @Estado 
                   WHERE Id_servicioAdicional = @Id_serviciosAdicionales";


            return ds.EjecutarConsulta(consulta, comando);
        }

        public bool CrearServicio(Servicios servicio)
        {
            SqlCommand comando = new SqlCommand();
            this.ArmarParametroServicio(comando, servicio);
            string consulta = "INSERT INTO ServiciosAdicionales(NombreServicio, Precio) VALUES (@NombreServicio, @Precio)";
            return ds.EjecutarConsulta(consulta, comando);
        }

    }
}
