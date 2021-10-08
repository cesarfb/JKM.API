using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Notification.ContactUs
{
    public class ContactUsNotificationCommand : INotification
    {
        public string EmailAddress { get; set; }
        public string Empresa { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Path { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Logo { get; set; }
    }
}
