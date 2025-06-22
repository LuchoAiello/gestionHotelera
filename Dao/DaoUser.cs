using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Dao
{
    public class DaoUser
    {
        AccesoDatos ds = new AccesoDatos();

        private void ArmarParametrosUsuario(SqlCommand Comando, Usuario user)
        {
            Comando.Parameters.AddWithValue("@Nombre", user.Nombre);
            Comando.Parameters.AddWithValue("@Contrasenia", user.Contrasenia);
            Comando.Parameters.AddWithValue("@Rol", user.Rol);
            Comando.Parameters.AddWithValue("@Estado", user.Estado);
            Comando.Parameters.AddWithValue("@Id_usuario", user.IdUsuario);
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
