using Cepdi.Domain.Entities;
using Cepdi.Persistence.Context;
using Dapper;
using System.Data;

namespace Cepdi.Persistence.Repositories
{
    public class UsuarioRepository : Cepdi.Application.Interfaces.IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> AddUsuarioAsync(Usuarios data)
        {
            var response = -1;
            string qry = "spActualizaUsuarios";
            var parameters = new DynamicParameters();
            parameters.Add("nombre", data.NOMBRE);
            parameters.Add("usuario", data.USUARIO);
            parameters.Add("psw", data.PASSWORD);
            parameters.Add("acciones", data.ACCIONES);

            using (var conn = this._context.CreateConnection)
            {
                response = await conn.ExecuteScalarAsync<int>(qry, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return response > 0;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await GetUsuariosByIdAsync(id);
            usuario.ESTATUS = false;
            return await this.UpdateUsuarioAsync(usuario);
        }

        public async Task<IEnumerable<Usuarios>> GetUsuariosAsync()
        {
            IEnumerable<Usuarios> response = null;
            string qry = "spAObtenerUsuarios";


            using (var conn = this._context.CreateConnection)
            {
                response = await conn.QueryAsync<Usuarios>(qry, commandType: System.Data.CommandType.StoredProcedure);
            }

            return response;
        }

        public async Task<Usuarios> GetUsuariosByIdAsync(int id)
        {

            IEnumerable<Usuarios> response = null;
            string qry = "spAObtenerUsuarios";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var conn = this._context.CreateConnection)
            {
                response = await conn.QueryAsync<Usuarios>(qry, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return response.FirstOrDefault();
        }

        public async Task<bool> UpdateUsuarioAsync(Usuarios data)
        {
            var response = -1;
            string qry = "spActualizaUsuarios";
            var parameters = new DynamicParameters();
            parameters.Add("idUsuario", data.IDUSUARIO);
            parameters.Add("nombre", data.NOMBRE);
            parameters.Add("usuario", data.USUARIO);
            parameters.Add("psw", data.PASSWORD);
            parameters.Add("status", data.ESTATUS);
            parameters.Add("acciones", data.ACCIONES);

            using (var conn = this._context.CreateConnection)
            {
                response = await conn.ExecuteScalarAsync<int>(qry, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return response > 0;
        }
    }
}
