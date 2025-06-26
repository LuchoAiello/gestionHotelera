using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public string NombreCompleto { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadHuespedes { get; set; }
        public decimal PrecioFinal { get; set; }
        public int Estado {  get; set; }


        public static Reserva Crear(int id, string nombre, string documento, string email, string telefono, DateTime fecha, int cantidad, decimal precio)
        {
            return new Reserva
            {
                IdReserva = id,
                NombreCompleto = nombre,
                Documento = documento,
                Email = email,
                Telefono = telefono,
                FechaReserva = fecha,
                CantidadHuespedes = cantidad,
                PrecioFinal = precio
            };
        }

        public class ReservaEnProceso
        {
            public int IdHuesped { get; set; }
            public List<int> IdHabitaciones { get; set; } = new List<int>();
            public DateTime FechaReserva { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public int CantidadHuespedes { get; set; }
            public List<int> ServiciosAdicionales { get; set; } = new List<int>();
            public int IdMetodoPago { get; set; }
            public decimal MontoTotal { get; set; }
            public string Observaciones { get; set; }
        }
    }
}
