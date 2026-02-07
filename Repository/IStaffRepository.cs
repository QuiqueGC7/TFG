using Models;

namespace TFG.Repositories
{
    public interface IStaffRepository
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task AddAsync(Staff staff);
        Task UpdateAsync(Staff staff);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}