using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Newtonsoft.Json;

namespace JKM.APPLICATION.Commands.Auth.LoginUser
{
    public class LoginUserCommand : IRequest<ResponseModel>
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class Validator : AbstractValidator<LoginUserCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El usuario no puede ser vacío");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña no puede ser vacía");
        }
    }
}
