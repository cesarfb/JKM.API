using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Notification.AceptarCotizacion
{
    public class AceptarCotizacionNotificationCommand : INotification
    {
        public CotizacionModel Cotizacion { get; set; }
        public IEnumerable<TipoTrabajadorModel> Trabajadores { get; set; }
        public IEnumerable<ActividadCotizancionTreeNode> Actividades { get; set; }
        public IEnumerable<DetalleOrdenModel> Productos { get; set; }
    }
}
