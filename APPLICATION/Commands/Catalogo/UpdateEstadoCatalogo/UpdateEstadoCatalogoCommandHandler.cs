using JKM.PERSISTENCE.Repository.Catalogo;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Catalogo.UpdateEstadoCatalogo
{
    public class UpdateEstadoCatalogoCommandHandler : IRequestHandler<UpdateEstadoCatalogoCommand, ResponseModel>
    {
        private readonly ICatalogoRepository _catalogoRepository;

        public UpdateEstadoCatalogoCommandHandler(ICatalogoRepository catalogoRepository)
        {
            _catalogoRepository = catalogoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateEstadoCatalogoCommand request, CancellationToken cancellationToken)
        {
            return await _catalogoRepository.UpdateEstadoCatalogo(request.IdCatalogo);
        }

    }

}
