using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Cotizacion.RechazarCotizacion
{
    public class RechazarCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdCotizacion { get; set; }
    }
}
