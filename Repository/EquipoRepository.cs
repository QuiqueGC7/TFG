using Npgsql;
using TFG.Models;

namespace TFG.Repositories
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly string _connectionString;

        public EquipoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Not found";
        }

        public async Task<List<Equipos>> GetAllAsync()
        {
            var equipos = new List<Equipos>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("SELECT idEquipo, nombre, victorias, derrotas FROM Equipos", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                equipos.Add(new Equipos
                {
                    IdEquipo = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Victorias = reader.GetInt32(2),
                    Derrotas = reader.GetInt32(3)
                });
            }
            return equipos;
        }

        public async Task<Equipos?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("SELECT idEquipo, nombre, victorias, derrotas FROM Equipos WHERE idEquipo = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Equipos
                {
                    IdEquipo = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Victorias = reader.GetInt32(2),
                    Derrotas = reader.GetInt32(3)
                };
            }
            return null;
        }

        public async Task AddAsync(Equipos equipo)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("INSERT INTO Equipos (nombre, victorias, derrotas) VALUES (@Nombre, @Victorias, @Derrotas)", connection);
            command.Parameters.AddWithValue("@Nombre", equipo.Nombre);
            command.Parameters.AddWithValue("@Victorias", equipo.Victorias);
            command.Parameters.AddWithValue("@Derrotas", equipo.Derrotas);
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Equipos equipo)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("UPDATE Equipos SET nombre = @Nombre, victorias = @Victorias, derrotas = @Derrotas WHERE idEquipo = @Id", connection);
            command.Parameters.AddWithValue("@Nombre", equipo.Nombre);
            command.Parameters.AddWithValue("@Victorias", equipo.Victorias);
            command.Parameters.AddWithValue("@Derrotas", equipo.Derrotas);
            command.Parameters.AddWithValue("@Id", equipo.IdEquipo);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("DELETE FROM Equipos WHERE idEquipo = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task InicializarDatosAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                INSERT INTO Equipos (nombre, victorias, derrotas) VALUES (@Nombre1, @Victorias1, @Derrotas1), (@Nombre2, @Victorias2, @Derrotas2)", connection);
            command.Parameters.AddWithValue("@Nombre1", "NacionalA2");
            command.Parameters.AddWithValue("@Victorias1", 10);
            command.Parameters.AddWithValue("@Derrotas1", 5);
            command.Parameters.AddWithValue("@Nombre2", "2Aragonesa");
            command.Parameters.AddWithValue("@Victorias2", 15);
            command.Parameters.AddWithValue("@Derrotas2", 3);
            await command.ExecuteNonQueryAsync();
        }
    }
}