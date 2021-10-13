using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<ResponseModel> LoginUser(AuthModel model);
        Task<ResponseModel> RegisterUser(AuthModel model);
    }
}
