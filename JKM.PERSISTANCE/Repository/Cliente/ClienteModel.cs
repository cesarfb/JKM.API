using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Cliente
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }

        public void RegisterCliente(string ruc = "", string razonSocial = "", string telefono = "")
        {
            RUC = ruc;
            RazonSocial = razonSocial;
            Telefono = telefono;
        }

        public void UpdateCliente(int idCliente = 0, string ruc = "", string razonSocial = "", string telefono = "")
        {
            IdCliente = idCliente;
            RUC = ruc;
            RazonSocial = razonSocial;
            Telefono = telefono;
        }
    }
}
