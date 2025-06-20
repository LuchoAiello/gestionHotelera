using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Servicios
    {
        private int Id_serviciosAdicionales;
        private string NombreServicio;
        private decimal Precio;
        private int Estado;

        public static Servicios CrearServicios(string nombre, decimal precio)
        {
            Servicios servicio = new Servicios();
            servicio.setNombre(nombre);
            servicio.setPrecio(precio);
            return servicio;
        }
        public static Servicios ModificarServicio(int id, string nombre,decimal precio, int estado)
        {
            Servicios servicio = new Servicios();
            servicio.setIdServicio(id);
            servicio.setNombre(nombre);
            servicio.setPrecio(precio);
            servicio.setEstado(estado);
            return servicio;
        }

        public int getIdServicio()
        {
            return Id_serviciosAdicionales;
        }
        public string getNombre()
        {
            return NombreServicio;
        }

        public decimal getPrecio()
        {
            return Precio;
        }


        public int getEstado()
        {
            return Estado;
        }

        public void setIdServicio(int id)
        {
            Id_serviciosAdicionales = id;
        }

        public void setNombre(string nombre)
        {
            NombreServicio = nombre;
        }

        public void setPrecio(decimal precio)
        {
            Precio = precio;
        }


        public void setEstado(int estado)
        {
            Estado = estado;
        }
    }
}
