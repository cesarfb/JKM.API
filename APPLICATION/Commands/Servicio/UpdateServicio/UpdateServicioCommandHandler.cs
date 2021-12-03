using JKM.PERSISTENCE.Repository.Servicio;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Servicio.UpdateServicio
{
    public class UpdateServicioCommandHandler : IRequestHandler<UpdateServicioCommand, ResponseModel>
    {
        private readonly IServicioRepository _servicioRepository;

        public UpdateServicioCommandHandler(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<ResponseModel> Handle(UpdateServicioCommand request, CancellationToken cancellationToken)
        {
            ServicioModel servicioModel = new ServicioModel();

            servicioModel.UpdateServicio(idServicio: request.IdServicio, nombre: request.Nombre, imagen: request.Imagen, descripcion: request.Descripcion);

            return await _servicioRepository.UpdateServicio(servicioModel);
        }
    }
}
