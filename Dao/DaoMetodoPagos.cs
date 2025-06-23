using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoMetodoPagos
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametrosMetodoPago(SqlCommand Comando, MetodoPago pago)
        {
            Comando.Parameters.AddWithValue("@Nombre", pago.getNombre());
            Comando.Parameters.AddWithValue("@Estado", pago.getEstado());
            Comando.Parameters.AddWithValue("@Id_metodoPago", pago.getIdMetodoPago());
        }
        public DataTable GetMetodoPagos()
        {
            string query = "SELECT * FROM MetodoPago";
            DataTable tabla = ds.ObtenerTabla("MetodoPago", query);
            return tabla;
        }

        public bool ModificarMetodoPago(MetodoPago pago)
        {
            SqlCommand comando = new SqlCommand();
            this.ArmarParametrosMetodoPago(comando, pago);
            string consulta = @"UPDATE MetodoPago 
                   SET Nombre = @Nombre, 
                       Estado = @Estado 
                   WHERE Id_metodoPago = @Id_metodoPago";
            return ds.EjecutarConsulta(consulta, comando);
        }

        public bool CrearMetodoPago(MetodoPago pago)
        {
            SqlCommand comando = new SqlCommand();
            this.ArmarParametrosMetodoPago(comando, pago);
            string consulta = "INSERT INTO MetodoPago(Nombre) VALUES (@Nombre)";
            return ds.EjecutarConsulta(consulta, comando);
        }
    }
}
