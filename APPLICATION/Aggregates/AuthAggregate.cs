using JKM.UTILITY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class AuthAggregate
    {
    }

    public class PerfilModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private DateTime FechaNacimiento { get; set; }
        [JsonProperty("fechaNacimiento")]
        public string FechaNacimientoString
        {
            get
            {
                return FechaNacimiento.ToString("yyyy-MM-dd");
            }
        }
        private int IdRol { get; set; }
        private string Descripcion { get; set; }
        public Identifier Rol
        {
            get
            {
                return new Identifier
                {
                    id = IdRol,
                    descripcion = Descripcion
                };
            }
        }
    }
}
