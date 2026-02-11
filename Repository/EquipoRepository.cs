using System.Data;
using Microsoft.Data.SqlClient;
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
            var Equipos = new List<Equipos>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT IdEquipo, Nombre, Victorias, Derrotas FROM Equipos";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Equipo = new Equipos
                            {
                                IdEquipo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Victorias = reader.GetInt32(2),
                                Derrotas = reader.GetInt32(3),
                            }; 

                            Equipos.Add(Equipo);
                        }
                    }
                }
            }
            return Equipos;
        }

        public async Task<Equipos> GetByIdAsync(int id)
        {
            Equipos equipo = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdEquipo, Nombre, Victorias, Derrotas FROM Equipos WHERE IdEquipo = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            equipo = new Equipos
                            {
                                IdEquipo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Victorias = reader.GetInt32(2),
                                Derrotas = reader.GetInt32(3),
                            };
                        }
                    }
                }
            }
            return equipo;
        }

        public async Task AddAsync(Equipos equipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Equipos (Nombre, Victorias, Derrotas) VALUES (@Nombre, @Victorias, @Derrotas)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                    command.Parameters.AddWithValue("@Victorias", equipo.Victorias);
                    command.Parameters.AddWithValue("@Derrotas", equipo.Derrotas);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Equipos equipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Equipos SET Nombre = @Nombre, Victorias = @Victorias, Derrotas = @Derrotas WHERE IdEquipo = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                    command.Parameters.AddWithValue("@Victorias", equipo.Victorias);
                    command.Parameters.AddWithValue("@Derrotas", equipo.Derrotas);
                    command.Parameters.AddWithValue("@Id", equipo.IdEquipo);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int idEquipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Equipos WHERE IdEquipo = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idEquipo);

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
                    INSERT INTO Equipos (Nombre, Victorias, Derrotas)
                    VALUES 
                    (@Nombre1, @Victorias1, @Derrotas1),
                    (@Nombre2, @Victorias2, @Derrotas2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "NacionalA2");
                    command.Parameters.AddWithValue("@Victorias1", 10);
                    command.Parameters.AddWithValue("@Derrotas1", 5);

                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "2Aragonesa");
                    command.Parameters.AddWithValue("@Victorias2", 15);
                    command.Parameters.AddWithValue("@Derrotas2", 3);


                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}