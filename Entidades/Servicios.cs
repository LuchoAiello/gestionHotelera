namespace Entidades
{
    public class Servicios
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public decimal Precio { get; set; }
        public int Estado { get; set; }

        public static Servicios CrearServicios(string nombre, decimal precio)
        {
            return new Servicios
            {
                NombreServicio = nombre,
                Precio = precio
            };
        }

        public static Servicios ModificarServicio(int id, string nombre, decimal precio, int estado)
        {
            return new Servicios
            {
                IdServicio = id,
                NombreServicio = nombre,
                Precio = precio,
                Estado = estado
            };
        }
    }
}

