using JKM.PERSISTENCE.Repository.Pedido;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Pedido.UpdateFechaEntrega
{
    public class UpdateFechaEntregaCommandHandler : IRequestHandler<UpdateFechaEntregaCommand, ResponseModel>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public UpdateFechaEntregaCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateFechaEntregaCommand request, CancellationToken cancellationToken)
        {
            return await _pedidoRepository.UpdateFechaRegistro(request.IdPedido, request.FechaEntrega);
        }
    }
}
