using System.Data;
using Microsoft.Data.SqlClient;

namespace TFG.Repositories
{
    public class JugadoresNacRepository : IJugadoresNacRepository
    {
        private readonly string _connectionString;

        public JugadoresNacRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MambaDB") ?? "Not found";
        }

        public async Task<List<JugadoresNac>> GetAllAsync()
        {
            var JugadoressNac = new List<JugadoresNac>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
//Falta Modificar las querys (Cuando se acabe la BDD)
                string query = "SELECT JugadorNacId, Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts FROM JugadoresA";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var JugadoresNac = new JugadoresNac
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

                            JugadoressNac.Add(JugadoresNac);
                        }
                    }
                }
            }
            return JugadoressNac;
        }

        public async Task<JugadoresNac> GetByIdAsync(int JugadorNacId)
        {
            JugadoresNac jugadoresNac = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts FROM JugadoresA WHERE JugadorAId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", JugadorNacId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            jugadoresNac = new JugadoresNac
                            {
                                JugadorNacId = reader.GetInt32(0),
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
            return jugadoresNac;
        }

        public async Task AddAsync(JugadoresNac jugadoresNac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO JugadoresA (Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, PorLibres, DosPts, TresPts) VALUES (@Nombre, @Dorsal, @Posicion, @Equipo, @Puntos, @Libres, @PorLibres, @DosPts, @TresPts)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", jugadoresNac.Nombre);
                    command.Parameters.AddWithValue("@Dorsal", jugadoresNac.Dorsal);
                    command.Parameters.AddWithValue("@Posicion", jugadoresNac.Posicion);
                    command.Parameters.AddWithValue("@Equipo", jugadoresNac.Equipo);
                    command.Parameters.AddWithValue("@Puntos", jugadoresNac.Puntos);
                    command.Parameters.AddWithValue("@Libres", jugadoresNac.Libres);
                    command.Parameters.AddWithValue("@PorLibres", jugadoresNac.PorLibres);
                    command.Parameters.AddWithValue("@DosPts", jugadoresNac.DosPts);
                    command.Parameters.AddWithValue("@TresPts", jugadoresNac.TresPts);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(JugadoresNac jugadoresNac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE JugadoresA SET Nombre = @Nombre, Dorsal = @Dorsal, Posicion = @Posicion, Equipo = @Equipo, Puntos = @Puntos, Libres = @Libres, PorLibres = @PorLibres, DosPts = @DosPts, TresPts = @TresPts WHERE JugadorAId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", jugadoresNac.JugadorAId);
                    command.Parameters.AddWithValue("@Nombre", jugadoresNac.Nombre);
                    command.Parameters.AddWithValue("@Dorsal", jugadoresNac.Dorsal);
                    command.Parameters.AddWithValue("@Posicion", jugadoresNac.Posicion);
                    command.Parameters.AddWithValue("@Equipo", jugadoresNac.Equipo);
                    command.Parameters.AddWithValue("@Puntos", jugadoresNac.Puntos);
                    command.Parameters.AddWithValue("@Libres", jugadoresNac.Libres);
                    command.Parameters.AddWithValue("@PorLibres", jugadoresNac.PorLibres);
                    command.Parameters.AddWithValue("@DosPts", jugadoresNac.DosPts);
                    command.Parameters.AddWithValue("@TresPts", jugadoresNac.TresPts);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int JugadorNacId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM JugadoresA WHERE JugadorAId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", JugadorNacId);

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