using Models;

namespace TFG.Repositories
{
    public interface IPatrocinadoresRepository
    {
        Task<List<Patrocinadores>> GetAllAsync();
        Task<Patrocinadores?> GetByIdAsync(int id);
        Task AddAsync(Patrocinadores patrocinadores);
        Task UpdateAsync(Patrocinadores patrocinadores);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}