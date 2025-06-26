using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleReserva
    {
        public int IdDetalleReserva { get; set; }
        public int IdReserva { get; set; }
        public int NumeroHabitacion { get; set; }
        public string Tipo { get; set; }
        public string ServiciosAdicionales { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal PrecioFinal { get; set; }

        // Método estático para crear objeto con solo lo que necesites
        public static DetalleReserva ModificarDetalle(int idDetalle, DateTime checkOut)
        {
            return new DetalleReserva
            {
                IdDetalleReserva = idDetalle,
                CheckOut = checkOut
            };
        }
    }

}
