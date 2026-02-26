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
                string query = "SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo, e.puntos, e.porLibres, e.por2Pts, e.por3Pts, e.valoracion, e.rebotes, e.asistencias FROM JugadoresNac j INNER JOIN EstadisticasJugadorNac e ON j.idJugador = e.idJugador";
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
                                Equipo = reader.GetInt32(4),
                                Puntos = reader.GetDouble(5),
                                Libres = reader.GetDouble(6), 
                                por2Pts = reader.GetDouble(7),
                                por3Pts = reader.GetDouble(8),
                                Valoracion = reader.GetDouble(9),
                                Rebotes = reader.GetDouble(10),
                                Asistencias = reader.GetDouble(11)
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

                string query = "SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo, e.puntos, e.porLibres, e.por2Pts, e.por3Pts, e.valoracion, e.rebotes, e.asistencias FROM JugadoresNac j INNER JOIN EstadisticasJugadorNac e ON j.idJugador = e.idJugador WHERE j.idJugador = @Id";
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
                                Equipo = reader.GetInt32(4),
                                Puntos = reader.GetDouble(5),
                                Libres = reader.GetDouble(6),
                                por2Pts = reader.GetDouble(7),
                                por3Pts = reader.GetDouble(8),
                                Valoracion = reader.GetDouble(9),
                                Rebotes = reader.GetDouble(10),
                                Asistencias = reader.GetDouble(11)
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
                using (var transaction = connection.BeginTransaction())
                {
                    string queryJugador = "INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal) VALUES (@Nombre, @Posicion, @Equipo, @Dorsal); SELECT SCOPE_IDENTITY();";
                    int nuevoId;
                    using (var cmd = new SqlCommand(queryJugador, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", jugadoresNac.Nombre);
                        cmd.Parameters.AddWithValue("@Posicion", jugadoresNac.Posicion);
                        cmd.Parameters.AddWithValue("@Equipo", jugadoresNac.Equipo);
                        cmd.Parameters.AddWithValue("@Dorsal", jugadoresNac.Dorsal);
                        nuevoId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }
                    string queryStats = "INSERT INTO EstadisticasJugadorNac (idJugador, puntos, porLibres, por2Pts, por3Pts, valoracion, rebotes, asistencias) VALUES (@Id, @Puntos, @Libres, @por2Pts, @por3Pts, @Valoracion, @Rebotes, @Asistencias)";
                    using (var cmd = new SqlCommand(queryStats, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", nuevoId);
                        cmd.Parameters.AddWithValue("@Puntos", jugadoresNac.Puntos);
                        cmd.Parameters.AddWithValue("@Libres", jugadoresNac.Libres);
                        cmd.Parameters.AddWithValue("@por2Pts", jugadoresNac.por2Pts);
                        cmd.Parameters.AddWithValue("@por3Pts", jugadoresNac.por3Pts);
                        cmd.Parameters.AddWithValue("@Valoracion", jugadoresNac.Valoracion);
                        cmd.Parameters.AddWithValue("@Rebotes", jugadoresNac.Rebotes);
                        cmd.Parameters.AddWithValue("@Asistencias", jugadoresNac.Asistencias);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    transaction.Commit();
                }
            }
        }

        public async Task UpdateAsync(JugadoresNac jugadoresNac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    string queryJugador = "UPDATE JugadoresNac SET nombre = @Nombre, posicion = @Posicion, idEquipo = @Equipo, dorsal = @Dorsal WHERE idJugador = @Id";
                    using (var cmd = new SqlCommand(queryJugador, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", jugadoresNac.JugadorNacId);
                        cmd.Parameters.AddWithValue("@Nombre", jugadoresNac.Nombre);
                        cmd.Parameters.AddWithValue("@Posicion", jugadoresNac.Posicion);
                        cmd.Parameters.AddWithValue("@Equipo", jugadoresNac.Equipo);
                        cmd.Parameters.AddWithValue("@Dorsal", jugadoresNac.Dorsal);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    string queryStats = "UPDATE EstadisticasJugadorNac SET puntos = @Puntos, porLibres = @Libres, por2Pts = @por2Pts, por3Pts = @por3Pts, valoracion = @Valoracion, rebotes = @Rebotes, asistencias = @Asistencias WHERE idJugador = @Id";
                    using (var cmd = new SqlCommand(queryStats, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", jugadoresNac.JugadorNacId);
                        cmd.Parameters.AddWithValue("@Puntos", jugadoresNac.Puntos);
                        cmd.Parameters.AddWithValue("@Libres", jugadoresNac.Libres);
                        cmd.Parameters.AddWithValue("@por2Pts", jugadoresNac.por2Pts);
                        cmd.Parameters.AddWithValue("@por3Pts", jugadoresNac.por3Pts);
                        cmd.Parameters.AddWithValue("@Valoracion", jugadoresNac.Valoracion);
                        cmd.Parameters.AddWithValue("@Rebotes", jugadoresNac.Rebotes);
                        cmd.Parameters.AddWithValue("@Asistencias", jugadoresNac.Asistencias);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    transaction.Commit();
                }
            }
        }

        public async Task DeleteAsync(int JugadorNacId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    string queryStats = "DELETE FROM EstadisticasJugadorNac WHERE idJugador = @Id";
                    using (var cmd = new SqlCommand(queryStats, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", JugadorNacId);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    string queryJugador = "DELETE FROM JugadoresNac WHERE idJugador = @Id";
                    using (var cmd = new SqlCommand(queryJugador, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", JugadorNacId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    transaction.Commit();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string Jugador1 = @"
                            INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal) 
                            VALUES (@Nombre1, @Posicion1, @idEquipo1, @Dorsal1); 
                            SELECT SCOPE_IDENTITY();";

                        int idJugador1;
                        using (var cmd = new SqlCommand(Jugador1, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Nombre1", "Sergio");
                            cmd.Parameters.AddWithValue("@Posicion1", "Base");
                            cmd.Parameters.AddWithValue("@idEquipo1", 2); 
                            cmd.Parameters.AddWithValue("@Dorsal1", 4);
                            idJugador1 = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }

                        string Stats1 = @"
                            INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
                            VALUES (@Id1, @Puntos1, @Valoracion1, @Rebotes1, @Asistencias1, @Libres1, @por2Pts1, @por3Pts1)";

                        using (var cmd = new SqlCommand(Stats1, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id1", idJugador1);
                            cmd.Parameters.AddWithValue("@Puntos1", 4.40);
                            cmd.Parameters.AddWithValue("@Valoracion1", 0.2);
                            cmd.Parameters.AddWithValue("@Rebotes1", 0.2);
                            cmd.Parameters.AddWithValue("@Asistencias1", 0.2);
                            cmd.Parameters.AddWithValue("@Libres1", 2.1);
                            cmd.Parameters.AddWithValue("@por2Pts1", 1.2);
                            cmd.Parameters.AddWithValue("@por3Pts1", 4.40);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        string Jugador2 = @"
                            INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal) 
                            VALUES (@Nombre2, @Posicion2, @idEquipo2, @Dorsal2); 
                            SELECT SCOPE_IDENTITY();";

                        int idJugador2;
                        using (var cmd = new SqlCommand(Jugador2, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Nombre2", "Varo");
                            cmd.Parameters.AddWithValue("@Posicion2", "Escolta");
                            cmd.Parameters.AddWithValue("@idEquipo2", 1);
                            cmd.Parameters.AddWithValue("@Dorsal2", 23);
                            idJugador2 = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }

                        string Stats2 = @"
                            INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
                            VALUES (@Id2, @Puntos2, @Valoracion2, @Rebotes2, @Asistencias2, @Libres2, @por2Pts2, @por3Pts2)";

                        using (var cmd = new SqlCommand(Stats2, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id2", idJugador2);
                            cmd.Parameters.AddWithValue("@Puntos2", 6.33);
                            cmd.Parameters.AddWithValue("@Valoracion2", 0.2);
                            cmd.Parameters.AddWithValue("@Rebotes2", 0.2);
                            cmd.Parameters.AddWithValue("@Asistencias2", 0.2);
                            cmd.Parameters.AddWithValue("@Libres2", 3.33);
                            cmd.Parameters.AddWithValue("@por2Pts2", 1.2);
                            cmd.Parameters.AddWithValue("@por3Pts2", 4.40);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}