using TFG.Repositories;

namespace TFG.Services
{
    public class JugadoresNacService : IJugadoresNacService
    {
        private readonly IJugadoresNacRepository _jugadoresNacRepository;

        public JugadoresNacService(IJugadoresNacRepository jugadoresNacRepository)
        {
            _jugadoresNacRepository = jugadoresNacRepository;
            
        }

        public async Task<List<JugadoresNac>> GetAllAsync()
        {
            return await _jugadoresNacRepository.GetAllAsync();
        }

        public async Task<JugadoresNac?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _jugadoresNacRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(JugadoresNac jugadoresNac)
        {
            if (string.IsNullOrWhiteSpace(jugadoresNac.Nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(jugadoresNac.Posicion))
                throw new ArgumentException("La posicion del jugador no puede estar vacío.");

            await _jugadoresNacRepository.AddAsync(jugadoresNac);
        }

        public async Task UpdateAsync(JugadoresNac jugadoresNac)
        {
            if (jugadoresNac.JugadorNacId <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(jugadoresNac.Nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío.");

            await _jugadoresNacRepository.UpdateAsync(jugadoresNac);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _jugadoresNacRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _jugadoresNacRepository.InicializarDatosAsync();
        }
    }
}
