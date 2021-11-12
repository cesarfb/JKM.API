using JKM.PERSISTENCE.Repository.Almacen;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Almacen.RegisterAlmacen
{
    public class RegisterAlmacenCommandHandler : IRequestHandler<RegisterAlmacenCommand, ResponseModel>
    {
        private readonly IAlmacenRepository _almacenRepository;

        public RegisterAlmacenCommandHandler(IAlmacenRepository almacenRepository)
        {
            _almacenRepository = almacenRepository;
        }

        public async Task<ResponseModel> Handle(RegisterAlmacenCommand request, CancellationToken cancellationToken)
        {
            AlmacenModel model = new AlmacenModel();
            model.Register(nombre: request.Nombre, direccion: request.Direccion, distrito: request.Distrito);
            return await _almacenRepository.RegisterAlmacen(model);
        }
    }
}
