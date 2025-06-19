using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class loggedUser
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
    public class Usuario
    {
        private int Id_usuario;
        private string Nombre;
        private string Contrasenia;
        private string Rol;
        private int Estado;

        public static Usuario CrearUsuario(string nombre, string Contrasenia, string rol)
        {
            Usuario user = new Usuario();
            user.setNombre(nombre);
            user.setPassword(Contrasenia);
            user.setRol(rol);
            return user;
        }

        public static Usuario ModificarUsuario(int id, string nombre, string Contrasenia, string rol, int estado)
        {
            Usuario user = new Usuario();
            user.setIdUsuario(id);
            user.setNombre(nombre);
            user.setPassword(Contrasenia);
            user.setRol(rol);
            user.setEstado(estado);
            return user;
        }


        public int getIdUsuario()
        {
            return Id_usuario;
        }
        public string getNombre()
        {
            return Nombre;
        }

        public string getContrasenia()
        {
            return Contrasenia;
        }
        public string getRol()
        {
            return Rol;
        }
        public int getEstado()
        {
            return Estado;
        }

        public void setIdUsuario(int id)
        {
            Id_usuario = id;
        }
        public void setNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void setPassword(string password)
        {
            Contrasenia = password;
        }
        public void setRol(string rol)
        {
            Rol = rol;
        }
        public void setEstado(int estado)
        {
            Estado = estado;
        }
    }
}
