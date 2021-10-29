using JKM.PERSISTENCE.Repository.Venta;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Venta.DeleteVenta
{
    public class DeleteVentaCommandHandler : IRequestHandler<DeleteVentaCommand, ResponseModel>
    {
        private readonly IVentaRepository _ventaRepository;

        public DeleteVentaCommandHandler(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<ResponseModel> Handle(DeleteVentaCommand request, CancellationToken cancellationToken)
        {

            return await _ventaRepository.DeleteVenta(request.IdVenta);
        }
    }
}
