using TFG.Repositories;

namespace TFG.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
            
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _staffRepository.GetAllAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _staffRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.Nombre))
                throw new ArgumentException("El nombre del staff no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(staff.Puesto))
                throw new ArgumentException("El nombre del staff no puede estar vacío.");

            await _staffRepository.AddAsync(staff);
        }

        public async Task UpdateAsync(Staff staff)
        {
            if (staff.IdStaff <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(staff.Nombre))
                throw new ArgumentException("El nombre del staff no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(staff.Puesto))
                throw new ArgumentException("El puesto del staff no puede estar vacío.");

            await _staffRepository.UpdateAsync(staff);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _staffRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _staffRepository.InicializarDatosAsync();
        }
    }
}
