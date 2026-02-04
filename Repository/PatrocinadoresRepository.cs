using System.Data;
using Microsoft.Data.SqlClient;

namespace TFG.Repositories
{
    public class PatrocinadoresRepository : IPatrocinadoresRepository
    {
        private readonly string _connectionString;

        public PatrocinadoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MambaDB") ?? "Not found";
        }

        public async Task<List<Patrocinadores>> GetAllAsync()
        {
            var patrocinadores = new List<Patrocinadores>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT PatrocinadorId, Nombre, CantidadAportada FROM Patrocinadores";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Patrocinadores = new Patrocinadores
                            {
                                PatrocinadorId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                CantidadAportada = reader.GetInt32(2)
                            }; 

                            patrocinadores.Add(Patrocinadores);
                        }
                    }
                }
            }
            return patrocinadores;
        }

        public async Task<Patrocinadores> GetByIdAsync(int PatrocinadorId)
        {
            Patrocinadores patrocinadores = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PatrocinadorId, Nombre, CantidadAportada FROM Patrocinadores WHERE PatrocinadorId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", PatrocinadorId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            patrocinadores = new Patrocinadores
                            {
                                PatrocinadorId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                CantidadAportada = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            return patrocinadores;
        }

        public async Task AddAsync(Patrocinadores patrocinadores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Patrocinadores (Nombre, CantidadAportada) VALUES (@Nombre, @CantidadAportada)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", patrocinadores.Nombre);
                    command.Parameters.AddWithValue("@CantidadAportada", patrocinadores.CantidadAportada);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Patrocinadores patrocinadores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Patrocinadores SET Nombre = @Nombre, CantidadAportada = @CantidadAportada WHERE PatrocinadorId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", patrocinadores.Nombre);
                    command.Parameters.AddWithValue("@CantidadAportada", patrocinadores.CantidadAportada);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int PatrocinadorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Patrocinadores WHERE PatrocinadorId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", PatrocinadorId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO Patrocinadores (Nombre, CantidadAportada)
                    VALUES 
                    (@Nombre1, @CantidadAportada1),
                    (@Nombre2, @CantidadAportada2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "Alfasa");
                    command.Parameters.AddWithValue("@CantidadAportada1", 4000);
                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "LaHora de Montecanal");
                    command.Parameters.AddWithValue("@CantidadAportada2", 1000);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}