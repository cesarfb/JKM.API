using JKM.PERSISTENCE.Utils;
using MediatR;
using System;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterCotizacion
{
    public class RegisterCotizacionCommand : IRequest<ResponseModel>
    {
        public string Solicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
    }
}
