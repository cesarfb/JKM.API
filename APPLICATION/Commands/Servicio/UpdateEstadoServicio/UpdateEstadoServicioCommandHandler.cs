using JKM.PERSISTENCE.Repository.Servicio;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Servicio.UpdateEstadoServicio
{
    public class UpdateEstadoServicioCommandHandler : IRequestHandler<UpdateEstadoServicioCommand, ResponseModel>
    {
        private readonly IServicioRepository _servicioRepository;

        public UpdateEstadoServicioCommandHandler(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<ResponseModel> Handle(UpdateEstadoServicioCommand request, CancellationToken cancellationToken)
        {
            return await _servicioRepository.UpdateEstadoServicio(request.IdServicio);
        }
    }

}
