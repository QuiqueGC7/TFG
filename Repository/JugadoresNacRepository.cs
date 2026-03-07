using Npgsql;

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
            var lista = new List<JugadoresNac>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo,
                       e.puntos, e.porLibres, e.por2Pts, e.por3Pts, e.valoracion, e.rebotes, e.asistencias
                FROM JugadoresNac j
                INNER JOIN EstadisticasJugadorNac e ON j.idJugador = e.idJugador", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new JugadoresNac
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
                });
            }
            return lista;
        }

        public async Task<JugadoresNac?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo,
                       e.puntos, e.porLibres, e.por2Pts, e.por3Pts, e.valoracion, e.rebotes, e.asistencias
                FROM JugadoresNac j
                INNER JOIN EstadisticasJugadorNac e ON j.idJugador = e.idJugador
                WHERE j.idJugador = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new JugadoresNac
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
            return null;
        }

        public async Task<IEnumerable<JugadoresNac>> GetByEquipoAsync(int equipoId)
        {
            var lista = new List<JugadoresNac>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo,
                       e.puntos, e.porLibres, e.por2Pts, e.por3Pts, e.valoracion, e.rebotes, e.asistencias
                FROM JugadoresNac j
                INNER JOIN EstadisticasJugadorNac e ON j.idJugador = e.idJugador
                WHERE j.idEquipo = @EquipoId", connection);
            command.Parameters.AddWithValue("@EquipoId", equipoId);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new JugadoresNac
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
                });
            }
            return lista;
        }

        public async Task AddAsync(JugadoresNac jugador)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            using var cmdJugador = new NpgsqlCommand(@"
                INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
                VALUES (@Nombre, @Posicion, @Equipo, @Dorsal)
                RETURNING idJugador", connection, transaction);
            cmdJugador.Parameters.AddWithValue("@Nombre", jugador.Nombre);
            cmdJugador.Parameters.AddWithValue("@Posicion", jugador.Posicion);
            cmdJugador.Parameters.AddWithValue("@Equipo", jugador.Equipo);
            cmdJugador.Parameters.AddWithValue("@Dorsal", jugador.Dorsal);
            var nuevoId = (int)(await cmdJugador.ExecuteScalarAsync())!;

            using var cmdStats = new NpgsqlCommand(@"
                INSERT INTO EstadisticasJugadorNac (idJugador, puntos, porLibres, por2Pts, por3Pts, valoracion, rebotes, asistencias)
                VALUES (@Id, @Puntos, @Libres, @por2Pts, @por3Pts, @Valoracion, @Rebotes, @Asistencias)", connection, transaction);
            cmdStats.Parameters.AddWithValue("@Id", nuevoId);
            cmdStats.Parameters.AddWithValue("@Puntos", jugador.Puntos);
            cmdStats.Parameters.AddWithValue("@Libres", jugador.Libres);
            cmdStats.Parameters.AddWithValue("@por2Pts", jugador.por2Pts);
            cmdStats.Parameters.AddWithValue("@por3Pts", jugador.por3Pts);
            cmdStats.Parameters.AddWithValue("@Valoracion", jugador.Valoracion);
            cmdStats.Parameters.AddWithValue("@Rebotes", jugador.Rebotes);
            cmdStats.Parameters.AddWithValue("@Asistencias", jugador.Asistencias);
            await cmdStats.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(JugadoresNac jugador)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            using var cmdJugador = new NpgsqlCommand(@"
                UPDATE JugadoresNac SET nombre=@Nombre, posicion=@Posicion, idEquipo=@Equipo, dorsal=@Dorsal
                WHERE idJugador=@Id", connection, transaction);
            cmdJugador.Parameters.AddWithValue("@Id", jugador.JugadorNacId);
            cmdJugador.Parameters.AddWithValue("@Nombre", jugador.Nombre);
            cmdJugador.Parameters.AddWithValue("@Posicion", jugador.Posicion);
            cmdJugador.Parameters.AddWithValue("@Equipo", jugador.Equipo);
            cmdJugador.Parameters.AddWithValue("@Dorsal", jugador.Dorsal);
            await cmdJugador.ExecuteNonQueryAsync();

            using var cmdStats = new NpgsqlCommand(@"
                UPDATE EstadisticasJugadorNac SET puntos=@Puntos, porLibres=@Libres, por2Pts=@por2Pts, por3Pts=@por3Pts,
                       valoracion=@Valoracion, rebotes=@Rebotes, asistencias=@Asistencias
                WHERE idJugador=@Id", connection, transaction);
            cmdStats.Parameters.AddWithValue("@Id", jugador.JugadorNacId);
            cmdStats.Parameters.AddWithValue("@Puntos", jugador.Puntos);
            cmdStats.Parameters.AddWithValue("@Libres", jugador.Libres);
            cmdStats.Parameters.AddWithValue("@por2Pts", jugador.por2Pts);
            cmdStats.Parameters.AddWithValue("@por3Pts", jugador.por3Pts);
            cmdStats.Parameters.AddWithValue("@Valoracion", jugador.Valoracion);
            cmdStats.Parameters.AddWithValue("@Rebotes", jugador.Rebotes);
            cmdStats.Parameters.AddWithValue("@Asistencias", jugador.Asistencias);
            await cmdStats.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            using var cmdStats = new NpgsqlCommand("DELETE FROM EstadisticasJugadorNac WHERE idJugador = @Id", connection, transaction);
            cmdStats.Parameters.AddWithValue("@Id", id);
            await cmdStats.ExecuteNonQueryAsync();
            using var cmdJugador = new NpgsqlCommand("DELETE FROM JugadoresNac WHERE idJugador = @Id", connection, transaction);
            cmdJugador.Parameters.AddWithValue("@Id", id);
            await cmdJugador.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }

        public async Task InicializarDatosAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            using var cmd1 = new NpgsqlCommand(@"
                INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal) VALUES ('Sergio', 'Base', 2, 4) RETURNING idJugador", connection, transaction);
            var id1 = (int)(await cmd1.ExecuteScalarAsync())!;
            using var stats1 = new NpgsqlCommand(@"
                INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
                VALUES (@id, 4.40, 0.2, 0.2, 0.2, 2.1, 1.2, 4.40)", connection, transaction);
            stats1.Parameters.AddWithValue("@id", id1);
            await stats1.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
        }
    }
}