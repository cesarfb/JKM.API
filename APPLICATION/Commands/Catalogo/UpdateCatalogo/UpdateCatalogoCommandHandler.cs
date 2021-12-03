using JKM.PERSISTENCE.Repository.Catalogo;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Catalogo.UpdateCatalogo
{
    public class UpdateCatalogoCommandHandler : IRequestHandler<UpdateCatalogoCommand, ResponseModel>
    {
        private readonly ICatalogoRepository _catalogoRepository;

        public UpdateCatalogoCommandHandler(ICatalogoRepository catalogoRepository)
        {
            _catalogoRepository = catalogoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateCatalogoCommand request, CancellationToken cancellationToken)
        {
            CatalogoModel catalogoModel = new CatalogoModel();

            catalogoModel.UpdateCatalogo(idCatalogo: request.IdCatalogo, precio: request.Precio, stock: request.Stock, idProducto: request.IdProducto);

            return await _catalogoRepository.UpdateCatalogo(catalogoModel);
        }
    }
}
