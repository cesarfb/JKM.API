namespace JKM.PERSISTENCE.Repository.Almacen
{
    public class AlmacenModel
    {
        public int IdAlmacen{ get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public void Register(string nombre, string direccion, string distrito)
        {
            Nombre=nombre;
            Direccion = direccion;
            Distrito = distrito;
        }
        public void Update(int idAlmacen,string nombre, string direccion, string distrito)
        {
            IdAlmacen = idAlmacen;
            Nombre = nombre;
            Direccion = direccion;
            Distrito = distrito;
        }
    }
}
