using JKM.UTILITY.Utils;
using System;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Pedido
{
    public interface IPedidoRepository
    {
        Task<ResponseModel> UpdateFechaRegistro(int idPedido, DateTime fechaEntrega);
        Task<ResponseModel> UpdateEstado(int idPedido, int idEstado);
    }
}
