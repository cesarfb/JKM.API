using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Catalogo
{
    public interface ICatalogoRepository
    {
        Task<ResponseModel> RegisterCatalogo(CatalogoModel catalogoModel);
        Task<ResponseModel> UpdateCatalogo(CatalogoModel catalogoModel);
        Task<ResponseModel> UpdateEstadoCatalogo(int idCatalogo);

    }
}
