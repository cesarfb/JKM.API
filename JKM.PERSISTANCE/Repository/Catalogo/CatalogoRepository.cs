using Dapper;
using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace JKM.PERSISTENCE.Repository.Catalogo
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly IDbConnection _conexion;

        public CatalogoRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterCatalogo(CatalogoModel catalogoModel)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Registro Tabla Catalogo
                    string insertCatalogo = $@"INSERT INTO Catalogo
                                                (precio, stock, idProducto)
                                            VALUES
                                                (@precio, @stock, @idProducto)";

                    int hasInsertCatalogo = await connection.ExecuteAsync(insertCatalogo, catalogoModel);

                    if (hasInsertCatalogo <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el Catalogo");

                    return Handlers.CloseConnection(connection, trans, "Se registro el catalogo");
                }

                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateCatalogo(CatalogoModel catalogoModel)
        {

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Comprobamos si existe el Producto ya registrado
                    string sql = $@"SELECT COUNT(1) FROM Catalogo WHERE idCatalogo != {catalogoModel.IdCatalogo}  and idProducto = {catalogoModel.IdProducto};";

                    int existeProducto = await connection.QueryFirstAsync<int>(sql);

                    if (existeProducto == 1)
                        Handlers.ExceptionClose(connection, "El Producto ya se encuentra registrado");

                    //Update Tabla Catalogo
                    string updateCatalogo = $@"UPDATE Catalogo SET
		                                          precio = @Precio,
                                                  stock = @Stock,
                                                  idProducto = @IdProducto
                                              WHERE 
                                                  idCatalogo = @IdCatalogo";

                    int hasUpdatedCatalogo = await connection.ExecuteAsync(updateCatalogo, catalogoModel);

                    if (hasUpdatedCatalogo <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el Catalogo");

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");

                }

                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateEstadoCatalogo(int idCatalogo)
        {

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Comprobamos el estado del Catalogo
                    string sql = $@"SELECT isActive
                                    FROM Catalogo
                                    WHERE idCatalogo = {idCatalogo};";

                    int estado = await connection.QueryFirstAsync<int>(sql);

                    //Si el Catalogo se encuentra en estado Activo
                    if (estado == 1)
                    {
                        //Update Estado en Tabla Catalogo
                        string updateEstadoCatalogo = $@"UPDATE Catalogo SET
		                                                    isActive = 0
                                                         WHERE 
                                                            idCatalogo = {idCatalogo}";

                        int hasUpdatedEstadoCatalogo = await connection.ExecuteAsync(updateEstadoCatalogo);

                        if (hasUpdatedEstadoCatalogo <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el estado del Catalogo");
                    }

                    //Si el Catalogo se encuentra en estado Inactivo
                    else if (estado == 0)
                    {
                        //Update Estado en Tabla Usuario
                        string updateEstadoCatalogo = $@"UPDATE Catalogo SET
		                                                    isActive = 1
                                                         WHERE 
                                                            idCatalogo = {idCatalogo}";

                        int hasUpdatedEstadoCatalogo = await connection.ExecuteAsync(updateEstadoCatalogo);

                        if (hasUpdatedEstadoCatalogo <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el estado del Catalogo");
                    }

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }

        }



    }
}
