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

                string query = "SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo, e.puntos, e.libres, e.porLibres, e.dosPts, e.tresPts FROM JugadoresArag j INNER JOIN EstadisticasJugadorArag e ON j.idJugador = e.idJugador";
                
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
                                Equipo = reader.GetInt32(4),         
                                Puntos = reader.GetDouble(5),       
                                Libres = reader.GetDouble(6),        
                                PorLibres = reader.GetDouble(7),     
                                DosPts = reader.GetDouble(8),        
                                TresPts = reader.GetDouble(9)        
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
    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = "SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo, e.puntos, e.libres, e.porLibres, e.dosPts, e.tresPts FROM JugadoresArag j INNER JOIN EstadisticasJugadorArag e ON j.idJugador = e.idJugador WHERE j.idJugador = @Id";
        
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", JugadorAId);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var JugadoresA = new JugadoresA
                    {
                        JugadorAId = reader.GetInt32(0),   
                        Nombre = reader.GetString(1),   
                        Dorsal = reader.GetInt32(2),     
                        Posicion = reader.GetString(3),       
                        Equipo = reader.GetInt32(4),         
                        Puntos = reader.GetDouble(5),        
                        Libres = reader.GetDouble(6),        
                        PorLibres = reader.GetDouble(7),     
                        DosPts = reader.GetDouble(8),        
                        TresPts = reader.GetDouble(9)         
                    }; 

                    return JugadoresA;
                }
            }
        }
    }
    return null;
}

        public async Task AddAsync(JugadoresA jugadoresA)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();
        // Usamos una transacción para insertar en las dos tablas de forma segura
        using (var transaction = connection.BeginTransaction())
        {
            try
            {
                string queryJugador = @"
                    INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal) 
                    VALUES (@Nombre, @Posicion, @idEquipo, @Dorsal);
                    SELECT SCOPE_IDENTITY();"; // Recupera el idJugador recién creado

                int nuevoId;
                using (var Jugador = new SqlCommand(queryJugador, connection, transaction))
                {
                    Jugador.Parameters.AddWithValue("@Nombre", jugadoresA.Nombre);
                    Jugador.Parameters.AddWithValue("@Posicion", jugadoresA.Posicion);
                    Jugador.Parameters.AddWithValue("@idEquipo", jugadoresA.Equipo); 
                    Jugador.Parameters.AddWithValue("@Dorsal", jugadoresA.Dorsal);

                    nuevoId = Convert.ToInt32(await Jugador.ExecuteScalarAsync());
                }

                //EstadisticasJugadorArag usando el nuevoId
                string queryStats = @"
                    INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
                    VALUES (@idJugador, @Puntos, @Libres, @PorLibres, @DosPts, @TresPts)";

                using (var Stats = new SqlCommand(queryStats, connection, transaction))
                {
                    Stats.Parameters.AddWithValue("@idJugador", nuevoId);
                    Stats.Parameters.AddWithValue("@Puntos", jugadoresA.Puntos);
                    Stats.Parameters.AddWithValue("@Libres", jugadoresA.Libres);
                    Stats.Parameters.AddWithValue("@PorLibres", jugadoresA.PorLibres);
                    Stats.Parameters.AddWithValue("@DosPts", jugadoresA.DosPts);
                    Stats.Parameters.AddWithValue("@TresPts", jugadoresA.TresPts);

                    await Stats.ExecuteNonQueryAsync();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // Re-lanzar el error para que el Controller lo maneje
            }
        }
    }
}

        public async Task UpdateAsync(JugadoresA jugadoresA)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        using (var transaction = connection.BeginTransaction())
        {
            string queryJugador = @"
                UPDATE JugadoresArag 
                SET nombre = @Nombre, 
                    dorsal = @Dorsal, 
                    posicion = @Posicion, 
                    idEquipo = @idEquipo 
                WHERE idJugador = @Id";

            using (var cmdJugador = new SqlCommand(queryJugador, connection, transaction))
            {
                cmdJugador.Parameters.AddWithValue("@Id", jugadoresA.JugadorAId);
                cmdJugador.Parameters.AddWithValue("@Nombre", jugadoresA.Nombre);
                cmdJugador.Parameters.AddWithValue("@Dorsal", jugadoresA.Dorsal);
                cmdJugador.Parameters.AddWithValue("@Posicion", jugadoresA.Posicion);
                cmdJugador.Parameters.AddWithValue("@idEquipo", jugadoresA.Equipo);

                await cmdJugador.ExecuteNonQueryAsync();
            }
            string queryStats = @"
                UPDATE EstadisticasJugadorArag 
                SET puntos = @Puntos, 
                    libres = @Libres, 
                    porLibres = @PorLibres, 
                    dosPts = @DosPts, 
                    tresPts = @TresPts 
                WHERE idJugador = @Id";

            using (var cmdStats = new SqlCommand(queryStats, connection, transaction))
            {
                cmdStats.Parameters.AddWithValue("@Id", jugadoresA.JugadorAId);
                cmdStats.Parameters.AddWithValue("@Puntos", jugadoresA.Puntos);
                cmdStats.Parameters.AddWithValue("@Libres", jugadoresA.Libres);
                cmdStats.Parameters.AddWithValue("@PorLibres", jugadoresA.PorLibres);
                cmdStats.Parameters.AddWithValue("@DosPts", jugadoresA.DosPts);
                cmdStats.Parameters.AddWithValue("@TresPts", jugadoresA.TresPts);

                await cmdStats.ExecuteNonQueryAsync();
            }

            transaction.Commit();
        }
    }
}

        public async Task DeleteAsync(int JugadorAId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    // 1º las estadísticas relacionadas
                    string queryStats = "DELETE FROM EstadisticasJugadorArag WHERE idJugador = @Id";
                    using (var cmdStats = new SqlCommand(queryStats, connection, transaction))
                    {
                        cmdStats.Parameters.AddWithValue("@Id", JugadorAId);
                        await cmdStats.ExecuteNonQueryAsync();
                    }
                    string queryJugador = "DELETE FROM JugadoresArag WHERE idJugador = @Id";
                    using (var cmdJugador = new SqlCommand(queryJugador, connection, transaction))
                    {
                        cmdJugador.Parameters.AddWithValue("@Id", JugadorAId);
                        await cmdJugador.ExecuteNonQueryAsync();
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
            string queryJugador1 = @"
                INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal) 
                VALUES ('Amit', 'Ala Pivot', 4, 4);
                SELECT SCOPE_IDENTITY();";

            int idJugador1;
            using (var cmd = new SqlCommand(queryJugador1, connection, transaction))
            {
                idJugador1 = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }

            string queryStats1 = @"
                INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
                VALUES (@id, 4.40, 2.1, 1.2, 4.40, 0.2)";
            
            using (var cmd = new SqlCommand(queryStats1, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@id", idJugador1);
                await cmd.ExecuteScalarAsync();
            }

            string queryJugador2 = @"
                INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal) 
                VALUES ('Mario', 'Escolta', 5, 23);
                SELECT SCOPE_IDENTITY();";

            int idJugador2;
            using (var cmd = new SqlCommand(queryJugador2, connection, transaction))
            {
                idJugador2 = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }

            string queryStats2 = @"
                INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
                VALUES (@id, 6.33, 3.33, 50.0, 2.40, 3.33)";
            
            using (var cmd = new SqlCommand(queryStats2, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@id", idJugador2);
                await cmd.ExecuteScalarAsync();
            }

            transaction.Commit();
        }
    }
}


    }

}