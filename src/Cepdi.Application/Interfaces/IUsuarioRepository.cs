using ENTITIES = Cepdi.Domain.Entities;


namespace Cepdi.Application.Interfaces
{
    public interface IUsuarioRepository : IUsuarioLogin
    {
        Task<IEnumerable<ENTITIES.Usuarios>> GetUsuariosAsync();
        Task<ENTITIES.Usuarios> GetUsuariosByIdAsync(int id);
        Task<bool> AddUsuarioAsync(ENTITIES.Usuarios data);
        Task<bool> UpdateUsuarioAsync(ENTITIES.Usuarios data);

        Task<bool> DeleteUsuarioAsync(int id);
    }

    public interface IUsuarioLogin
    {
        Task<ENTITIES.Usuarios> GetUsuarioByLoginAsync(string usuario, string password);
    }
}
