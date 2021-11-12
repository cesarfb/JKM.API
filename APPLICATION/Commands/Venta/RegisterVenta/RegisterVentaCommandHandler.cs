﻿using JKM.PERSISTENCE.Repository.Venta;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Venta.RegisterVenta
{
    public class RegisterVentaCommandHandler : IRequestHandler<RegisterVentaCommand, ResponseModel>
    {
        private readonly IVentaRepository _ventaRepository;

        public RegisterVentaCommandHandler(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<ResponseModel> Handle(RegisterVentaCommand request, CancellationToken cancellationToken)
        {
            VentaModel ventaModel = new VentaModel();

            ventaModel.RegisterVenta(pagoParcial: request.PagoParcial, fechaCuota: request.FechaCuota, idVenta: request.IdVenta );

            return await _ventaRepository.RegisterVenta(ventaModel);
        }
    }
}
