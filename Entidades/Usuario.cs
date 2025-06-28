namespace Entidades
{
    public class LoggedUser
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; }
        public int Estado { get; set; }

        public static Usuario CrearUsuario(string nombre, string contrasenia, string rol)
        {
            return new Usuario
            {
                Nombre = nombre,
                Contrasenia = contrasenia,
                Rol = rol
            };
        }

        public static Usuario ModificarUsuario(int id, string nombre, string contrasenia, string rol)
        {
            return new Usuario
            {
                IdUsuario = id,
                Nombre = nombre,
                Contrasenia = contrasenia,
                Rol = rol,
            };
        }

        public static Usuario EliminarUsuario(int id)
        {
            return new Usuario
            {
                IdUsuario = id,
            };
        }
    }
}
