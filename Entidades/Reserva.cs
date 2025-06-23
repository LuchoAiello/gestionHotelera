using System;

namespace Entidades
{
    public class Reserva
    {
        public int Id_reserva { get; set; }
        public int Id_huesped { get; set; }
        public int? Id_pago { get; set; }
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadHuespedes { get; set; }
        public decimal? PrecioFinal { get; set; }
        public bool Pago { get; set; }
    }
}
