using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Cotizacion.AceptarCotizacion
{
    public class AceptarCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdCotizacion { get; set; }
    }
}
