using TFG.Repositories;

namespace TFG.Services
{
    public class PatrocinadoresService : IPatrocinadoresService
    {
        private readonly IPatrocinadoresRepository _patrocinadoresRepository;

        public PatrocinadoresService(IPatrocinadoresRepository patrocinadoresRepository)
        {
            _patrocinadoresRepository = patrocinadoresRepository;
            
        }

        public async Task<List<Patrocinadores>> GetAllAsync()
        {
            return await _patrocinadoresRepository.GetAllAsync();
        }

        public async Task<Patrocinadores?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _patrocinadoresRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Patrocinadores patrocinadores)
        {
            if (string.IsNullOrWhiteSpace(patrocinadores.Nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío.");

            if (patrocinadores.CantidadAportada <= 0)
                throw new ArgumentException("La cantidad aportada debe ser mayor que cero.");

            
            await _patrocinadoresRepository.AddAsync(patrocinadores);
        }

        public async Task UpdateAsync(Patrocinadores patrocinadores)
        {
            if (patrocinadores.PatrocinadorId <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(patrocinadores.Nombre))
                throw new ArgumentException("El nombre del patrocinador no puede estar vacío.");

            await _patrocinadoresRepository.UpdateAsync(patrocinadores);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _patrocinadoresRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _patrocinadoresRepository.InicializarDatosAsync();
        }
    }
}
