using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cliente;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cliente.RegisterCliente
{
    public class RegisterClienteCommandHandler : IRequestHandler<RegisterClienteCommand, ResponseModel>
    {
        private readonly IClienteRepository _clienteRepository;
        public RegisterClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseModel> Handle(RegisterClienteCommand request, CancellationToken cancellationToken)
        {
            ClienteModel clienteModel = new ClienteModel();
            clienteModel.RegisterCliente(razonSocial: request.RazonSocial, ruc: request.RUC,
                telefono: request.Telefono);

            return await _clienteRepository.RegisterCliente(clienteModel);
        }
    }
}
