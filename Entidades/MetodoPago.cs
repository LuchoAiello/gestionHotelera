using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }

        public static MetodoPago CrearMetodoPago(string nombre)
        {
            return new MetodoPago
            {
                Nombre = nombre
            };
        }

        public static MetodoPago ModificarMetodoPago(int id, string nombre, int estado)
        {
            return new MetodoPago
            {
                IdMetodoPago = id,
                Nombre = nombre,
                Estado = estado
            };
        }
    }
}
