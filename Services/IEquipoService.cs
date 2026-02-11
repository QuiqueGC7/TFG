using TFG.Models;

namespace TFG.Services
{
    public interface IEquipoService
    {
        Task<List<Equipos>> GetAllAsync();
        Task<Equipos?> GetByIdAsync(int id);
        Task AddAsync(Equipos equipo);
        Task UpdateAsync(Equipos equipo);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
