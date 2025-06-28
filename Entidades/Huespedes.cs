using System;
using System.Xml.Linq;

namespace Entidades
{
    public class Huespedes
    {
        public int IdHuesped { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public static Huespedes CrearHuesped(string nombre, string apellido, string documento, string tipoDocumento, string email, string telefono, DateTime fechaNacimiento)
        {
            return new Huespedes
            {
                Nombre = nombre,
                Apellido = apellido,
                Documento = documento,
                TipoDocumento = tipoDocumento,
                Email = email,
                Telefono = telefono,
                FechaNacimiento = fechaNacimiento,
            };
        }

        public static Huespedes ModificarHuesped(int id, string nombre, string apellido, string documento, string tipoDocumento, string email, string telefono, DateTime fechaNacimiento)
        {
            return new Huespedes
            {
                IdHuesped = id,
                Nombre = nombre,
                Apellido = apellido,
                Documento = documento,
                TipoDocumento = tipoDocumento,
                Email = email,
                Telefono = telefono,
                FechaNacimiento = fechaNacimiento
            };
        }

        public static Huespedes EliminarHuesped(int id)
        {
            return new Huespedes
            {
                IdHuesped = id
            };
        }
    }
}
