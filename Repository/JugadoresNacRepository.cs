using System.Data;
using Microsoft.Data.SqlClient;
using TFG.Models;

namespace TFG.Repositories
{
    public class JugadoresNacRepository : IJugadoresNacRepository
    {
        private readonly string _connectionString;

        public JugadoresNacRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Not found";
        }

        public async Task<List<JugadoresNac>> GetAllAsync()
        {
            var JugadoressNac = new List<JugadoresNac>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT JugadorNacId, Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, por2Pts, por3Pts, Valoracion, Rebotes, Asistencias FROM JugadoresNac";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var JugadoresNac = new JugadoresNac
                            {
                                JugadorNacId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Dorsal = reader.GetInt32(2),
                                Posicion = reader.GetString(3),
                                Equipo = reader.GetString(4),
                                Puntos = (double)reader.GetDecimal(5),
                                Libres = reader.GetInt32(6),
                                por2Pts = (double)reader.GetDecimal(7),
                                por3Pts = (double)reader.GetDecimal(8),
                                Valoracion = (double)reader.GetDecimal(9),
                                Rebotes = (double)reader.GetDecimal(10),
                                Asistencias = (double)reader.GetDecimal(11)
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

                string query = "SELECT JugadorNacId, Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, por2Pts, por3Pts, Valoracion, Rebotes, Asistencias FROM JugadoresNac WHERE JugadorNacId = @Id";
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
                                Dorsal = reader.GetInt32(2),
                                Posicion = reader.GetString(3),
                                Equipo = reader.GetString(4),
                                Puntos = (double)reader.GetDecimal(5),
                                Libres = reader.GetInt32(6),
                                por2Pts = (double)reader.GetDecimal(7),
                                por3Pts = (double)reader.GetDecimal(8),
                                Valoracion = (double)reader.GetDecimal(9),
                                Rebotes = (double)reader.GetDecimal(10),
                                Asistencias = (double)reader.GetDecimal(11)
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

                string query = "INSERT INTO JugadoresNac (Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, por2Pts, por3Pts, Valoracion, Rebotes, Asistencias) VALUES (@Nombre, @Dorsal, @Posicion, @Equipo, @Puntos, @Libres, @por2Pts, @por3Pts, @Valoracion, @Rebotes, @Asistencias)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", jugadoresNac.Nombre);
                    command.Parameters.AddWithValue("@Dorsal", jugadoresNac.Dorsal);
                    command.Parameters.AddWithValue("@Posicion", jugadoresNac.Posicion);
                    command.Parameters.AddWithValue("@Equipo", jugadoresNac.Equipo);
                    command.Parameters.AddWithValue("@Puntos", jugadoresNac.Puntos);
                    command.Parameters.AddWithValue("@Libres", jugadoresNac.Libres);
                    command.Parameters.AddWithValue("@por2Pts", jugadoresNac.por2Pts);
                    command.Parameters.AddWithValue("@por3Pts", jugadoresNac.por3Pts);
                    command.Parameters.AddWithValue("@Valoracion", jugadoresNac.Valoracion);
                    command.Parameters.AddWithValue("@Rebotes", jugadoresNac.Rebotes);
                    command.Parameters.AddWithValue("@Asistencias", jugadoresNac.Asistencias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(JugadoresNac jugadoresNac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE JugadoresNac SET Nombre = @Nombre, Dorsal = @Dorsal, Posicion = @Posicion, Equipo = @Equipo, Puntos = @Puntos, Libres = @Libres, por2Pts = @por2Pts, por3Pts = @por3Pts, Valoracion = @Valoracion, Rebotes = @Rebotes, Asistencias = @Asistencias WHERE JugadorNacId = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", jugadoresNac.Nombre);
                    command.Parameters.AddWithValue("@Dorsal", jugadoresNac.Dorsal);
                    command.Parameters.AddWithValue("@Posicion", jugadoresNac.Posicion);
                    command.Parameters.AddWithValue("@Equipo", jugadoresNac.Equipo);
                    command.Parameters.AddWithValue("@Puntos", jugadoresNac.Puntos);
                    command.Parameters.AddWithValue("@Libres", jugadoresNac.Libres);
                    command.Parameters.AddWithValue("@por2Pts", jugadoresNac.por2Pts);
                    command.Parameters.AddWithValue("@por3Pts", jugadoresNac.por3Pts);
                    command.Parameters.AddWithValue("@Valoracion", jugadoresNac.Valoracion);
                    command.Parameters.AddWithValue("@Rebotes", jugadoresNac.Rebotes);
                    command.Parameters.AddWithValue("@Asistencias", jugadoresNac.Asistencias);


                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int JugadorNacId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM JugadoresNac WHERE JugadorNacId = @Id";
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
                    INSERT INTO JugadoresNac (Nombre, Dorsal, Posicion, Equipo, Puntos, Libres, por2Pts, por3Pts, Valoracion, Rebotes, Asistencias)
                    VALUES 
                    (@Nombre1, @Dorsal1, @Posicion1, @Equipo1, @Puntos1, @Libres1, @por2Pts1, @por3Pts1, @Valoracion1, @Rebotes1, @Asistencias1),
                    (@Nombre2, @Dorsal2, @Posicion2, @Equipo2, @Puntos2, @Libres2, @por2Pts2, @por3Pts2, @Valoracion2, @Rebotes2, @Asistencias2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "Sergio");
                    command.Parameters.AddWithValue("@Dorsal1", 4);
                    command.Parameters.AddWithValue("@Posicion1", "Base");
                    command.Parameters.AddWithValue("@Equipo1", "NacionalA2");
                    command.Parameters.AddWithValue("@Puntos1", 4.40);
                    command.Parameters.AddWithValue("@Libres1", 2.1);
                    command.Parameters.AddWithValue("@por2Pts1", 1.2);
                    command.Parameters.AddWithValue("@por3Pts1", 4.40);
                    command.Parameters.AddWithValue("@Valoracion1", 0.2);
                    command.Parameters.AddWithValue("@Rebotes1", 0.2);
                    command.Parameters.AddWithValue("@Asistencias1", 0.2);

                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "Varo");
                    command.Parameters.AddWithValue("@Dorsal2", 23);
                    command.Parameters.AddWithValue("@Posicion2", "Escolta");
                    command.Parameters.AddWithValue("@Equipo2", "NacionalA1");
                    command.Parameters.AddWithValue("@Puntos2", 6.33);
                    command.Parameters.AddWithValue("@Libres2", 3.33);
                    command.Parameters.AddWithValue("@por2Pts2", 1.2);
                    command.Parameters.AddWithValue("@por3Pts2", 4.40);
                    command.Parameters.AddWithValue("@Valoracion2", 0.2);
                    command.Parameters.AddWithValue("@Rebotes2", 0.2);
                    command.Parameters.AddWithValue("@Asistencias2", 0.2);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}