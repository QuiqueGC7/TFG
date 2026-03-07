using Npgsql;

namespace TFG.Repositories
{
    public class PatrocinadoresRepository : IPatrocinadoresRepository
    {
        private readonly string _connectionString;

        public PatrocinadoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Not found";
        }

        public async Task<List<Patrocinadores>> GetAllAsync()
        {
            var lista = new List<Patrocinadores>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("SELECT PatrocinadorId, Nombre, CantidadAportada, Email, Telefono FROM Patrocinadores", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Patrocinadores
                {
                    PatrocinadorId = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    CantidadAportada = reader.GetInt32(2),
                    Email = reader.GetString(3),
                    Telefono = (int)reader.GetInt64(4)
                });
            }
            return lista;
        }

        public async Task<Patrocinadores?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("SELECT PatrocinadorId, Nombre, CantidadAportada, Email, Telefono FROM Patrocinadores WHERE PatrocinadorId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Patrocinadores
                {
                    PatrocinadorId = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    CantidadAportada = reader.GetInt32(2),
                    Email = reader.GetString(3),
                    Telefono = (int)reader.GetInt64(4)
                };
            }
            return null;
        }

        public async Task AddAsync(Patrocinadores p)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("INSERT INTO Patrocinadores (Nombre, CantidadAportada, Email, Telefono) VALUES (@Nombre, @Cantidad, @Email, @Telefono)", connection);
            command.Parameters.AddWithValue("@Nombre", p.Nombre);
            command.Parameters.AddWithValue("@Cantidad", p.CantidadAportada);
            command.Parameters.AddWithValue("@Email", p.Email);
            command.Parameters.AddWithValue("@Telefono", (long)p.Telefono);
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Patrocinadores p)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("UPDATE Patrocinadores SET Nombre=@Nombre, CantidadAportada=@Cantidad, Email=@Email, Telefono=@Telefono WHERE PatrocinadorId=@Id", connection);
            command.Parameters.AddWithValue("@Nombre", p.Nombre);
            command.Parameters.AddWithValue("@Cantidad", p.CantidadAportada);
            command.Parameters.AddWithValue("@Email", p.Email);
            command.Parameters.AddWithValue("@Telefono", (long)p.Telefono);
            command.Parameters.AddWithValue("@Id", p.PatrocinadorId);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand("DELETE FROM Patrocinadores WHERE PatrocinadorId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task InicializarDatosAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                INSERT INTO Patrocinadores (Nombre, CantidadAportada, Email, Telefono)
                VALUES (@Nombre1, @Cantidad1, @Email1, @Telefono1), (@Nombre2, @Cantidad2, @Email2, @Telefono2)", connection);
            command.Parameters.AddWithValue("@Nombre1", "Alfasa");
            command.Parameters.AddWithValue("@Cantidad1", 4000);
            command.Parameters.AddWithValue("@Email1", "basket@aro.com");
            command.Parameters.AddWithValue("@Telefono1", 743892173L);
            command.Parameters.AddWithValue("@Nombre2", "LaHora de Montecanal");
            command.Parameters.AddWithValue("@Cantidad2", 1000);
            command.Parameters.AddWithValue("@Email2", "empresa@hola.com");
            command.Parameters.AddWithValue("@Telefono2", 643903128L);
            await command.ExecuteNonQueryAsync();
        }
    }
}