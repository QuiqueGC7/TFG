using TFG.Repositories;

namespace TFG.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly IEquipoRepository _equipoRepository;

        public EquipoService(IEquipoRepository equipoRepository)
        {
            _equipoRepository = equipoRepository;
            
        }

        public async Task<List<Equipo>> GetAllAsync()
        {
            return await _equipoRepository.GetAllAsync();
        }

        public async Task<Equipo?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _equipoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Equipo equipo)
        {
            if (string.IsNullOrWhiteSpace(equipo.Nombre))
                throw new ArgumentException("El nombre del equipo no puede estar vacío.");

            if (equipo.Victorias < 0)
                throw new ArgumentException("El número de victorias no puede ser negativo.");

            if (equipo.Derrotas < 0)
                throw new ArgumentException("El número de derrotas no puede ser negativo.");

            await _equipoRepository.AddAsync(equipo);
        }

        public async Task UpdateAsync(Equipo equipo)
        {
            if (equipo.IdEquipo <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(equipo.Nombre))
                throw new ArgumentException("El nombre del equipo no puede estar vacío.");

            await _equipoRepository.UpdateAsync(equipo);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _equipoRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _equipoRepository.InicializarDatosAsync();
        }
    }
}
