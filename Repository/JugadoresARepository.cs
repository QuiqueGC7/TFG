using System.Data;
using Microsoft.Data.SqlClient;
using TFG.Models;

namespace TFG.Repositories
{
    public class JugadoresARepository : IJugadoresARepository
    {
        private readonly string _connectionString;

        public JugadoresARepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Not found";
        }

        public async Task<List<JugadoresA>> GetAllAsync()
        {
            var JugadoressA = new List<JugadoresA>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT JugadorAId, Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts FROM JugadoresA";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var JugadoresA = new JugadoresA
                            {
                                JugadorAId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Dorsal = reader.GetInt32(2),
                                Posicion = reader.GetString(3),
                                Equipo = reader.GetString(4),
                                Puntos = (double)reader.GetDecimal(5),
                                Libres = reader.GetInt32(6),
                                PorLibres = (double)reader.GetDecimal(7),
                                DosPts = (double)reader.GetDecimal(8),
                                TresPts = (double)reader.GetDecimal(9),
                            }; 

                            JugadoressA.Add(JugadoresA);
                        }
                    }
                }
            }
            return JugadoressA;
        }

        public async Task<JugadoresA> GetByIdAsync(int JugadorAId)
        {
            JugadoresA jugadoresA = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts FROM JugadoresA WHERE JugadorAId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", JugadorAId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            jugadoresA = new JugadoresA
                            {
                                JugadorAId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Dorsal = reader.GetInt32(2),
                                Posicion = reader.GetString(3),
                                Equipo = reader.GetString(4),
                                Puntos = (double)reader.GetDecimal(5),
                                Libres = reader.GetInt32(6),
                                PorLibres = (double)reader.GetDecimal(7),
                                DosPts = (double)reader.GetDecimal(8),
                                TresPts = (double)reader.GetDecimal(9),
                            };
                        }
                    }
                }
            }
            return jugadoresA;
        }

        public async Task AddAsync(JugadoresA jugadoresA)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO JugadoresA (Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts) VALUES (@Nombre, @Dorsal, @Posicion, @Equipo, @Puntos, @Libres, @PorLibres, @DosPts, @TresPts)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", jugadoresA.Nombre);
                    command.Parameters.AddWithValue("@Dorsal", jugadoresA.Dorsal);
                    command.Parameters.AddWithValue("@Posicion", jugadoresA.Posicion);
                    command.Parameters.AddWithValue("@Equipo", jugadoresA.Equipo);
                    command.Parameters.AddWithValue("@Puntos", jugadoresA.Puntos);
                    command.Parameters.AddWithValue("@Libres", jugadoresA.Libres);
                    command.Parameters.AddWithValue("@PorLibres", jugadoresA.PorLibres);
                    command.Parameters.AddWithValue("@DosPts", jugadoresA.DosPts);
                    command.Parameters.AddWithValue("@TresPts", jugadoresA.TresPts);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(JugadoresA jugadoresA)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE JugadoresA SET Nombre = @Nombre, Dorsal = @Dorsal, Posicion = @Posicion, Equipo = @Equipo, Puntos = @Puntos, Libres = @Libres, PorLibres = @PorLibres, DosPts = @DosPts, TresPts = @TresPts WHERE JugadorAId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", jugadoresA.JugadorAId);
                    command.Parameters.AddWithValue("@Nombre", jugadoresA.Nombre);
                    command.Parameters.AddWithValue("@Dorsal", jugadoresA.Dorsal);
                    command.Parameters.AddWithValue("@Posicion", jugadoresA.Posicion);
                    command.Parameters.AddWithValue("@Equipo", jugadoresA.Equipo);
                    command.Parameters.AddWithValue("@Puntos", jugadoresA.Puntos);
                    command.Parameters.AddWithValue("@Libres", jugadoresA.Libres);
                    command.Parameters.AddWithValue("@PorLibres", jugadoresA.PorLibres);
                    command.Parameters.AddWithValue("@DosPts", jugadoresA.DosPts);
                    command.Parameters.AddWithValue("@TresPts", jugadoresA.TresPts);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int JugadorAId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM JugadoresA WHERE JugadorAId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", JugadorAId);

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
                    INSERT INTO JugadoresA (Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts)
                    VALUES 
                    (@Nombre1, @Dorsal1, @Posicion1, @Equipo1, @Puntos1, @Libres1, @PorLibres1, @DosPts1, @TresPts1),
                    (@Nombre2, @Dorsal2, @Posicion2, @Equipo2, @Puntos2, @Libres2, @PorLibres2, @DosPts2, @TresPts2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "Amit");
                    command.Parameters.AddWithValue("@Dorsal1", 4);
                    command.Parameters.AddWithValue("@Posicion1", "Ala Pivot");
                    command.Parameters.AddWithValue("@Equipo1", "2ªAragonesa");
                    command.Parameters.AddWithValue("@Puntos1", 4.40);
                    command.Parameters.AddWithValue("@Libres1", 2.1);
                    command.Parameters.AddWithValue("@PorLibres1", 1.2);
                    command.Parameters.AddWithValue("@DosPts1", 4.40);
                    command.Parameters.AddWithValue("@TresPts1", 0.2);

                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "Mario");
                    command.Parameters.AddWithValue("@Dorsal2", 23);
                    command.Parameters.AddWithValue("@Posicion2", "Escolta");
                    command.Parameters.AddWithValue("@Equipo2", "3ªAragonesa");
                    command.Parameters.AddWithValue("@Puntos2", 6.33);
                    command.Parameters.AddWithValue("@Libres2", 3.33);
                    command.Parameters.AddWithValue("@PorLibres2", "Bebida mojada");
                    command.Parameters.AddWithValue("@DosPts2", 2.40);
                    command.Parameters.AddWithValue("@TresPts2", 3.33);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}