using JKM.PERSISTENCE.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class TipoTrabajador
    {
        public int id { get; set; }
        public decimal precioReferencial { get; set; }
        public string descripcion { get; set; }
    }

    public class TrabajadorModel
    {
        public int IdTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private DateTime? FechaNacimiento { get; set; }
        public string FechaNacimientoString
        {
            get
            {
                return FechaNacimiento?.ToString("yyyy-MM-dd");
            }
        }
        private int IdEstado { get; set; }
        private string DescripcionEstado { get; set; }
        public Identifier Estado
        {
            get
            {
                return new Identifier
                {
                    id = IdEstado,
                    descripcion = DescripcionEstado
                };
            }
        }
        private int IdTipoTrabajador { get; set; }
        private string DescripcionTipo { get; set; }
        private decimal PrecioReferencial { get; set; }

        public TipoTrabajador Tipo
        {
            get
            {
                return new TipoTrabajador
                {
                    id = IdTipoTrabajador,
                    descripcion = DescripcionTipo,
                    precioReferencial = PrecioReferencial
                };
            }
        }
    }
}
