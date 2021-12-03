namespace JKM.PERSISTENCE.Repository.Producto
{
    public class ProductoModel
    {
        public int? IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public void RegisterProducto(string nombre, string codigo)
        {
            Nombre = nombre;
            Codigo = codigo;
        }

        public void UpdateProducto(int? idProducto, string nombre, string codigo)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Codigo = codigo;
        }
    }
}
