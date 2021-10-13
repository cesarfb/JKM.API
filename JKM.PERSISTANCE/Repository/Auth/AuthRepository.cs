using Dapper;
using JKM.UTILITY.Jwt;
using JKM.UTILITY.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace JKM.PERSISTENCE.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _conexion;
        private readonly IJwtService _jwtService;
        public AuthRepository(IDbConnection conexion, IJwtService jwtService)
        {
            _conexion = conexion;
            _jwtService = jwtService;
        }
        public async Task<ResponseModel> LoginUser(AuthModel model)
        {
            string sql = $@"SELECT idUsuario
                            FROM Usuario
                            WHERE username = @Username
                            AND password = @Password;";

            using (IDbConnection connection = _conexion)
            {
                connection.Open();

                int idUser = await connection.QueryFirstOrDefaultAsync<int>(sql, model);

                if (idUser <= 0)
                    Handlers.ExceptionClose(connection, "Credenciales incorrectas");

                ResponseModel response = Handlers.CloseConnection(connection, null, "Credenciales correctas");
                response.Data = _jwtService.CreateToken(idUser, model.Username);
                return response;
            }
        }

        public async Task<ResponseModel> RegisterUser(AuthModel model)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM Usuario
                            WHERE username = @Username";
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int exist = await connection.QueryFirstAsync<int>(sql, model);

                    if (exist > 0)
                        Handlers.ExceptionClose(connection, "El nombre de usuario ya se encuentra registrado");

                    string insertDetalle = $@"INSERT INTO DetalleUsuario
                                            (nombre, apellido, fechaNacimiento)
                                            VALUES 
                                            (@Nombre, @Apellido, @FechaNacimiento);
                                            SELECT SCOPE_IDENTITY()";

                    model.IdDetalleUsuario = await connection.QueryFirstOrDefaultAsync<int>(insertDetalle, model);

                    if (model.IdDetalleUsuario <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al insertar el detalle del usuario");

                    string insertUsuario = $@"INSERT INTO Usuario
                                            (username, password, idDetalleUsuario, idRol)
                                            VALUES 
                                            (@Username, @Password, @IdDetalleUsuario, 1);
                                            SELECT SCOPE_IDENTITY()";

                    model.IdUsuario = await connection.QueryFirstOrDefaultAsync<int>(insertUsuario, model);

                    if (model.IdUsuario <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al registrar al usuario");

                    return Handlers.CloseConnection(connection, trans, "Registro exitoso");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
