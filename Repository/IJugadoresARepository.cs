using Models;

namespace TFG.Repositories
{
    public interface IJugadoresARepository
    {
        Task<List<JugadoresA>> GetAllAsync();
        Task<JugadoresA?> GetByIdAsync(int id);
        Task AddAsync(JugadoresA jugadoresA);
        Task UpdateAsync(JugadoresA jugadoresA);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}