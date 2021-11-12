using FluentValidation;
using JKM.PERSISTENCE.Repository.Almacen;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Almacen.UpdateAlmacen
{
    public class UpdateAlmacenCommandHandler : IRequestHandler<UpdateAlmacenCommand, ResponseModel>
    {
        private readonly IAlmacenRepository _almacenRepository;

        public UpdateAlmacenCommandHandler(IAlmacenRepository almacenRepository)
        {
            _almacenRepository = almacenRepository;
        }

        public async Task<ResponseModel> Handle(UpdateAlmacenCommand request, CancellationToken cancellationToken)
        {
            AlmacenModel model = new AlmacenModel();
            model.Update(idAlmacen: request.IdAlmacen, nombre: request.Nombre, direccion: request.Direccion, distrito: request.Distrito);
            return await _almacenRepository.UpdateAlmacen(model);
        }
    }
}