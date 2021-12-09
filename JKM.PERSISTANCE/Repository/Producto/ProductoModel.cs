namespace JKM.PERSISTENCE.Repository.Producto
{
    public class ProductoModel
    {
        public int? IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Imagen { get; set; }

        public void RegisterProducto(string nombre, string codigo, string imagen)
        {
            Nombre = nombre;
            Codigo = codigo;
            Imagen = imagen;
        }

        public void UpdateProducto(int? idProducto, string nombre, string codigo, string imagen)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Codigo = codigo;
            Imagen = imagen;
        }
    }
}
