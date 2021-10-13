using JKM.PERSISTENCE.Repository.Auth;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Auth.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        public RegisterUserCommandHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<ResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            AuthModel model = new AuthModel();
            model.Register(username: request.Username, password: request.Password, nombre: request.Nombre,
                apellido: request.Apellido, fechaNacimiento: request.FechaNacimiento);
            return await _authRepository.RegisterUser(model);
        }
    }
}
