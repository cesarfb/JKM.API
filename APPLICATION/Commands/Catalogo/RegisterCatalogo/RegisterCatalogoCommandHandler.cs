using JKM.PERSISTENCE.Repository.Catalogo;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Catalogo.RegisterCatalogo
{
    public class RegisterCatalogoCommandHandler : IRequestHandler<RegisterCatalogoCommand, ResponseModel>
    {
        private readonly ICatalogoRepository _catalogoRepository;

        public RegisterCatalogoCommandHandler(ICatalogoRepository catalogoRepository)
        {
            _catalogoRepository = catalogoRepository;
        }

        public async Task<ResponseModel> Handle(RegisterCatalogoCommand request, CancellationToken cancellationToken)
        {
            CatalogoModel catalogoModel = new CatalogoModel();

            catalogoModel.RegisterCatalogo(precio: request.Precio, stock: request.Stock, idProducto: request.IdProducto);

            return await _catalogoRepository.RegisterCatalogo(catalogoModel);
        }
    }
}
