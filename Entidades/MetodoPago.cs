namespace Entidades
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }
        public string Nombre { get; set; }

        public static MetodoPago CrearMetodoPago(string nombre)
        {
            return new MetodoPago
            {
                Nombre = nombre
            };
        }

        public static MetodoPago ModificarMetodoPago(int id, string nombre)
        {
            return new MetodoPago
            {
                IdMetodoPago = id,
                Nombre = nombre,
            };
        }

        public static MetodoPago EliminarMetodoPago(int id)
        {
            return new MetodoPago
            {
                IdMetodoPago = id,
            };
        }
    }
}
