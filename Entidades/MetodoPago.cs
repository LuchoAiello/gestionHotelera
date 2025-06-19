using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MetodoPago
    {
        private int Id_metodoPago;
        private string Nombre;
        private int Estado;

        public static MetodoPago CrearMetodoPago(string nombre)
        {
            MetodoPago pago = new MetodoPago();
            pago.setNombre(nombre);
            return pago;
        }
        public static MetodoPago ModificarMetodoPago(int id, string nombre, int estado)
        {
            MetodoPago pago = new MetodoPago();
            pago.setIdMetodoPago(id);
            pago.setNombre(nombre);
            pago.setEstado(estado);
            return pago;
        }

        public int getIdMetodoPago()
        {
            return Id_metodoPago;
        }
        public string getNombre()
        {
            return Nombre;
        }

        public int getEstado()
        {
            return Estado;
        }

        public void setIdMetodoPago(int id)
        {
            Id_metodoPago = id;
        }

        public void setNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void setEstado(int estado)
        {
            Estado = estado;
        }

    }
}
