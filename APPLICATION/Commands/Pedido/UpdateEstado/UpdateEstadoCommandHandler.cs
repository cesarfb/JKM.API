using JKM.PERSISTENCE.Repository.Pedido;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Pedido.UpdateEstado
{
    public class UpdateEstadoCommandHandler : IRequestHandler<UpdateEstadoCommand, ResponseModel>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public UpdateEstadoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateEstadoCommand request, CancellationToken cancellationToken)
        {
            return await _pedidoRepository.UpdateEstado(request.IdPedido, request.IdEstado);
        }
    }
}