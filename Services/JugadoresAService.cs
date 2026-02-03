using TFG.Repositories;

namespace TFG.Services
{
    public class JugadoresAService : IJugadoresAService
    {
        private readonly IJugadoresARepository _jugadoresARepository;

        public JugadoresAService(IJugadoresARepository jugadoresARepository)
        {
            _jugadoresARepository = jugadoresARepository;
            
        }

        public async Task<List<JugadoresA>> GetAllAsync()
        {
            return await _jugadoresARepository.GetAllAsync();
        }

        public async Task<JugadoresA?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _jugadoresARepository.GetByIdAsync(id);
        }

        public async Task AddAsync(JugadoresA jugadoresA)
        {
            if (string.IsNullOrWhiteSpace(jugadoresA.Nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(jugadoresA.Posicion))
                throw new ArgumentException("La posicion del jugador no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(jugadoresA.Equipo))
                throw new ArgumentException("El equipo del jugador no puede estar vacío.");

            await _jugadoresARepository.AddAsync(jugadoresA);
        }

        public async Task UpdateAsync(JugadoresA jugadoresA)
        {
            if (jugadoresA.JugadorAId <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(jugadoresA.Nombre))
                throw new ArgumentException("El nombre del jugador no puede estar vacío.");

            await _jugadoresARepository.UpdateAsync(jugadoresA);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _jugadoresARepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _jugadoresARepository.InicializarDatosAsync();
        }
    }
}
