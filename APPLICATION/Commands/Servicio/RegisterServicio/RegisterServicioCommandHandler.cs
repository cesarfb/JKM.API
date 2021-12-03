using JKM.PERSISTENCE.Repository.Servicio;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Servicio.RegisterServicio
{
    public class RegisterServicioCommandHandler : IRequestHandler<RegisterServicioCommand, ResponseModel>
    {
        private readonly IServicioRepository _servicioRepository;

        public RegisterServicioCommandHandler(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<ResponseModel> Handle(RegisterServicioCommand request, CancellationToken cancellationToken)
        {
            ServicioModel servicioModel = new ServicioModel();

            servicioModel.RegisterServicio(nombre: request.Nombre, imagen: request.Imagen, descripcion: request.Descripcion);

            return await _servicioRepository.RegisterServicio(servicioModel);
        }


    }
}
