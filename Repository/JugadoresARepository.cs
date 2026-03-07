using Npgsql;

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
            var lista = new List<JugadoresA>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo,
                       e.puntos, e.libres, e.porLibres, e.dosPts, e.tresPts
                FROM JugadoresArag j
                INNER JOIN EstadisticasJugadorArag e ON j.idJugador = e.idJugador", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new JugadoresA
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
                });
            }
            return lista;
        }

        public async Task<JugadoresA?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo,
                       e.puntos, e.libres, e.porLibres, e.dosPts, e.tresPts
                FROM JugadoresArag j
                INNER JOIN EstadisticasJugadorArag e ON j.idJugador = e.idJugador
                WHERE j.idJugador = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new JugadoresA
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
            }
            return null;
        }

        public async Task<IEnumerable<JugadoresA>> GetByEquipoAsync(int equipoId)
        {
            var lista = new List<JugadoresA>();
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(@"
                SELECT j.idJugador, j.nombre, j.dorsal, j.posicion, j.idEquipo,
                       e.puntos, e.libres, e.porLibres, e.dosPts, e.tresPts
                FROM JugadoresArag j
                INNER JOIN EstadisticasJugadorArag e ON j.idJugador = e.idJugador
                WHERE j.idEquipo = @EquipoId", connection);
            command.Parameters.AddWithValue("@EquipoId", equipoId);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new JugadoresA
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
                });
            }
            return lista;
        }

        public async Task AddAsync(JugadoresA jugador)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            // PostgreSQL usa RETURNING para obtener el id generado
            using var cmdJugador = new NpgsqlCommand(@"
                INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
                VALUES (@Nombre, @Posicion, @Equipo, @Dorsal)
                RETURNING idJugador", connection, transaction);
            cmdJugador.Parameters.AddWithValue("@Nombre", jugador.Nombre);
            cmdJugador.Parameters.AddWithValue("@Posicion", jugador.Posicion);
            cmdJugador.Parameters.AddWithValue("@Equipo", jugador.Equipo);
            cmdJugador.Parameters.AddWithValue("@Dorsal", jugador.Dorsal);
            var nuevoId = (int)(await cmdJugador.ExecuteScalarAsync())!;

            using var cmdStats = new NpgsqlCommand(@"
                INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
                VALUES (@Id, @Puntos, @Libres, @PorLibres, @DosPts, @TresPts)", connection, transaction);
            cmdStats.Parameters.AddWithValue("@Id", nuevoId);
            cmdStats.Parameters.AddWithValue("@Puntos", jugador.Puntos);
            cmdStats.Parameters.AddWithValue("@Libres", jugador.Libres);
            cmdStats.Parameters.AddWithValue("@PorLibres", jugador.PorLibres);
            cmdStats.Parameters.AddWithValue("@DosPts", jugador.DosPts);
            cmdStats.Parameters.AddWithValue("@TresPts", jugador.TresPts);
            await cmdStats.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }

        public async Task UpdateAsync(JugadoresA jugador)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            using var cmdJugador = new NpgsqlCommand(@"
                UPDATE JugadoresArag SET nombre=@Nombre, dorsal=@Dorsal, posicion=@Posicion, idEquipo=@Equipo
                WHERE idJugador=@Id", connection, transaction);
            cmdJugador.Parameters.AddWithValue("@Id", jugador.JugadorAId);
            cmdJugador.Parameters.AddWithValue("@Nombre", jugador.Nombre);
            cmdJugador.Parameters.AddWithValue("@Dorsal", jugador.Dorsal);
            cmdJugador.Parameters.AddWithValue("@Posicion", jugador.Posicion);
            cmdJugador.Parameters.AddWithValue("@Equipo", jugador.Equipo);
            await cmdJugador.ExecuteNonQueryAsync();

            using var cmdStats = new NpgsqlCommand(@"
                UPDATE EstadisticasJugadorArag SET puntos=@Puntos, libres=@Libres, porLibres=@PorLibres, dosPts=@DosPts, tresPts=@TresPts
                WHERE idJugador=@Id", connection, transaction);
            cmdStats.Parameters.AddWithValue("@Id", jugador.JugadorAId);
            cmdStats.Parameters.AddWithValue("@Puntos", jugador.Puntos);
            cmdStats.Parameters.AddWithValue("@Libres", jugador.Libres);
            cmdStats.Parameters.AddWithValue("@PorLibres", jugador.PorLibres);
            cmdStats.Parameters.AddWithValue("@DosPts", jugador.DosPts);
            cmdStats.Parameters.AddWithValue("@TresPts", jugador.TresPts);
            await cmdStats.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            using var cmdStats = new NpgsqlCommand("DELETE FROM EstadisticasJugadorArag WHERE idJugador = @Id", connection, transaction);
            cmdStats.Parameters.AddWithValue("@Id", id);
            await cmdStats.ExecuteNonQueryAsync();
            using var cmdJugador = new NpgsqlCommand("DELETE FROM JugadoresArag WHERE idJugador = @Id", connection, transaction);
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
                INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal) VALUES ('Amit', 'Ala Pivot', 4, 4) RETURNING idJugador", connection, transaction);
            var id1 = (int)(await cmd1.ExecuteScalarAsync())!;
            using var stats1 = new NpgsqlCommand(@"
                INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts) VALUES (@id, 4.40, 2.1, 1.2, 4.40, 0.2)", connection, transaction);
            stats1.Parameters.AddWithValue("@id", id1);
            await stats1.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
        }
    }
}