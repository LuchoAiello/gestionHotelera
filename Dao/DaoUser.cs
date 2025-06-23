using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Dao
{
    public class DaoUser
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametrosUsuario(SqlCommand Comando, Usuario user)
        {
            Comando.Parameters.AddWithValue("@Nombre", user.getNombre());
            Comando.Parameters.AddWithValue("@Contrasenia", user.getContrasenia());
            Comando.Parameters.AddWithValue("@Rol", user.getRol());
            Comando.Parameters.AddWithValue("@Estado", user.getEstado());
            Comando.Parameters.AddWithValue("@Id_usuario", user.getIdUsuario());
        }
        public DataTable GetUser()
        {
            string query = "SELECT * FROM Usuarios";
            DataTable tabla = ds.ObtenerTabla("Usuarios", query);
            return tabla;
        }

        public bool ModificarUsuario(Usuario user)
        {
            SqlCommand comando = new SqlCommand();
            this.ArmarParametrosUsuario(comando, user);
            string consulta = @"UPDATE Usuarios 
                   SET Nombre = @Nombre, 
                       Contrasenia = @Contrasenia, 
                       Rol = @Rol, 
                       Estado = @Estado 
                   WHERE Id_usuario = @Id_usuario";


            return ds.EjecutarConsulta(consulta, comando);
        }

        public bool CrearUsuario(Usuario user)
        {
            SqlCommand comando = new SqlCommand();
            this.ArmarParametrosUsuario(comando, user);
            string consulta = "INSERT INTO Usuarios(Nombre, Contrasenia, Rol) VALUES (@Nombre, @Contrasenia, @Rol)";


            return ds.EjecutarConsulta(consulta, comando);
        }
    }
}
