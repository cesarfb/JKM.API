using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cliente;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cliente.UpdateCliente
{
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, ResponseModel>
    {
        private readonly IClienteRepository _clienteRepository;
        public UpdateClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseModel> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            ClienteModel clienteModel = new ClienteModel();
            clienteModel.UpdateCliente(idCliente: request.IdCliente, razonSocial: request.RazonSocial, ruc: request.RUC,
                telefono: request.Telefono);

            return await _clienteRepository.UpdateCliente(clienteModel);
        }
    }
}
