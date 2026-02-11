using TFG.Models;

namespace TFG.Services
{
    public interface IStaffService
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task AddAsync(Staff staff);
        Task UpdateAsync(Staff staff);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
