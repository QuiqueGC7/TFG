using Models;

namespace TFG.Repositories
{
    public interface IJugadoresNacRepository
    {
        Task<List<JugadoresNac>> GetAllAsync();
        Task<JugadoresNac?> GetByIdAsync(int id);
        Task AddAsync(JugadoresNac jugadoresNac);
        Task UpdateAsync(JugadoresNac jugadoresNac);
        Task<IEnumerable<JugadoresNac>> GetByEquipoAsync(int idEquipo);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}