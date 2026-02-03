namespace TFG.Services
{
    public interface IJugadoresNacService
    {
        Task<List<JugadoresNac>> GetAllAsync();
        Task<JugadoresNac?> GetByIdAsync(int id);
        Task AddAsync(JugadoresNac jugadoresNac);
        Task UpdateAsync(JugadoresNac jugadoresNac);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
