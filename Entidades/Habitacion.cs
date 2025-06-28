using System;

namespace Entidades
{
    public class Habitaciones
    {
        public int Id_habitacion { get; set; }
        public int NumeroHabitacion { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

        public static Habitaciones CrearHabitacion(int numeroHabitacion, string tipo, int capacidad, string estado, decimal precio, string descripcion)
        {
            return new Habitaciones
            {
                NumeroHabitacion = numeroHabitacion,
                Tipo = tipo,
                Capacidad = capacidad,
                Estado = estado,
                Precio = precio,
                Descripcion = descripcion,
            };
        }

        public static Habitaciones ModificarHabitacion(int id, int numeroHabitacion, string tipo, int capacidad, string estado, decimal precio, string descripcion)
        {
            return new Habitaciones
            {
                Id_habitacion = id,
                NumeroHabitacion = numeroHabitacion,
                Tipo = tipo,
                Capacidad = capacidad,
                Estado = estado,
                Precio = precio,
                Descripcion = descripcion,
            };
        }

    }
}
