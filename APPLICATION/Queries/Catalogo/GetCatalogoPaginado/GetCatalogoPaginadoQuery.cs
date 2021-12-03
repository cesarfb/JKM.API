using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Catalogo.GetCatalogoPaginado
{
    public class GetCatalogoPaginadoQuery : IRequest<PaginadoResponse<CatalogoModel>>
    {
    }
}
