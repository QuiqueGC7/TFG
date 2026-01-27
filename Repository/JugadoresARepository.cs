using System.Data;
using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{
    public class JugadoresARepository : IJugadoresARepository
    {
        private readonly string _connectionString;

        public JugadoresARepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MambaDB") ?? "Not found";
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
                                Dorsal = (double)reader.GetDecimal(2),
                                Posicion = reader.GetBoolean(3),
                                Equipo = reader.GetString(4),
                                Puntos = reader.GetDecimal(5),
                                Libres = reader.GetInt32(6),
                                PorLibres = reader.GetDecimal(7),
                                DosPts = reader.GetDecimal(8),
                                TresPts = reader.GetDecimal(9),
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
                                Dorsal = (double)reader.GetDecimal(2),
                                Posicion = reader.GetBoolean(3),
                                Equipo = reader.GetString(4),
                                Puntos = reader.GetDecimal(5),
                                Libres = reader.GetInt32(6),
                                PorLibres = reader.GetDecimal(7),
                                DosPts = reader.GetDecimal(8),
                                TresPts = reader.GetDecimal(9),
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

                string query = "INSERT INTO Bebida (Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts) VALUES (@Nombre, @Dorsal, @Posicion, @Equipo, @Puntos, @Libres, @PorLibres, @DosPts, @TresPutos)";
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

                string query = "UPDATE Bebida SET Nombre = @Nombre, Precio = @Precio, EsAlcoholica = @EsAlcoholica WHERE JugadorAId = @Id";
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

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Bebida WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

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
                    INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
                    VALUES 
                    (@Nombre1, @Precio1, @EsAlcoholica1),
                    (@Nombre2, @Precio2, @EsAlcoholica2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "Bebida mojada");
                    command.Parameters.AddWithValue("@Precio1", 4.40);
                    command.Parameters.AddWithValue("@EsAlcoholica1", 1);

                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "Bebida húmeda");
                    command.Parameters.AddWithValue("@Precio2", 5.70);
                    command.Parameters.AddWithValue("@EsAlcoholica2", 0);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}