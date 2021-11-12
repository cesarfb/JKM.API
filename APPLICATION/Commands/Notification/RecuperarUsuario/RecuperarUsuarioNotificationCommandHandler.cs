using Dapper;
using JKM.APPLICATION.Utils;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Notification.RecuperarUsuario
{
    public class RecuperarUsuarioNotificationCommandHandler : INotificationHandler<RecuperarUsuarioNotificationCommand>
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;
        private readonly IDbConnection _conexion;

        public RecuperarUsuarioNotificationCommandHandler(SmtpClient smtp, MailMessage mail, IDbConnection conexion)
        {
            _smtp = smtp;
            _mail = mail;
            _conexion = conexion;
        }

        public async Task Handle(RecuperarUsuarioNotificationCommand request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT DU.nombre, DU.apellido, 
                            U.username, U.password
                            FROM DetalleUsuario DU
                            INNER JOIN Usuario U ON (U.idDetalleUsuario = DU.idDetalleUsuario)
                            WHERE DU.email = @Email;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    RecuperarUsuarioModel usuario = await connection.QueryFirstOrDefaultAsync<RecuperarUsuarioModel>(sql, request);

                    connection.Close();

                    if (usuario == null)
                        Handlers.ExceptionClose(connection, "No se encontro una cuenta vinculada al correo");

                    using (MailMessage mail = _mail)
                    {
                        mail.To.Add(request.Email);
                        mail.Subject = "Recuperar usuario";
                        mail.Body = Templates.RecuperarUsuarioHtml(usuario);
                        using (SmtpClient smtp = _smtp)
                        {
                            await smtp.SendMailAsync(mail);
                        }
                    }
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
