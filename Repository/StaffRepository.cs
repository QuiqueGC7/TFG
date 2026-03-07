using Npgsql;
using TFG.Models;

namespace TFG.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly string _connectionString;

        public StaffRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Not found";
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            var lista = new List<Staff>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("SELECT idStaff, nombre, puesto FROM Staff", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Staff
                {
                    IdStaff = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Puesto = reader.GetString(2)
                });
            }
            return lista;
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("SELECT idStaff, nombre, puesto FROM Staff WHERE idStaff = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Staff
                {
                    IdStaff = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Puesto = reader.GetString(2)
                };
            }
            return null;
        }

        public async Task AddAsync(Staff staff)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("INSERT INTO Staff (nombre, puesto) VALUES (@Nombre, @Puesto)", connection);
            command.Parameters.AddWithValue("@Nombre", staff.Nombre);
            command.Parameters.AddWithValue("@Puesto", staff.Puesto);
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("UPDATE Staff SET nombre=@Nombre, puesto=@Puesto WHERE idStaff=@Id", connection);
            command.Parameters.AddWithValue("@Nombre", staff.Nombre);
            command.Parameters.AddWithValue("@Puesto", staff.Puesto);
            command.Parameters.AddWithValue("@Id", staff.IdStaff);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("DELETE FROM Staff WHERE idStaff = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task InicializarDatosAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("INSERT INTO Staff (nombre, puesto) VALUES (@Nombre1, @Puesto1), (@Nombre2, @Puesto2)", connection);
            command.Parameters.AddWithValue("@Nombre1", "LLeyda");
            command.Parameters.AddWithValue("@Puesto1", "Entrenador");
            command.Parameters.AddWithValue("@Nombre2", "Mario");
            command.Parameters.AddWithValue("@Puesto2", "Presidente");
            await command.ExecuteNonQueryAsync();
        }
    }
}