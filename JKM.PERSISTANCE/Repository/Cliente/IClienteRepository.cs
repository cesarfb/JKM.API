using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Cliente
{
    public interface IClienteRepository
    {
        Task<ResponseModel> RegisterCliente(ClienteModel clienteModel);
        Task<ResponseModel> UpdateCliente(ClienteModel clienteModel);
    }
}
