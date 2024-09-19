using ENTITIES = Cepdi.Domain.Entities;


namespace Cepdi.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<ENTITIES.Usuarios>> GetUsuariosAsync();
        Task<ENTITIES.Usuarios> GetUsuariosByIdAsync(int id);
        Task<bool> AddUsuarioAsync(ENTITIES.Usuarios data);
        Task<bool> UpdateUsuarioAsync(ENTITIES.Usuarios data);

        Task<bool> DeleteUsuarioAsync(int id);
    }
}
