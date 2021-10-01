using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionById
{
    public class GetCotizacionByIdQuery : IRequest<CotizacionModel>
    {
        public int IdCotizacion { get; set; }
    }
}
