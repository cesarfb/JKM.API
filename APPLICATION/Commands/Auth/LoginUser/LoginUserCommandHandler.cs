using JKM.PERSISTENCE.Repository.Auth;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Auth.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        public LoginUserCommandHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<ResponseModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            AuthModel model = new AuthModel();
            model.Login(request.Username, request.Password);
            return await _authRepository.LoginUser(model);
        }
    }
}
