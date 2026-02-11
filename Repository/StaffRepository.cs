using System.Data;
using Microsoft.Data.SqlClient;
using TFG.Models;

namespace TFG.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly string _connectionString;

        public StaffRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MambaDB") ?? "Not found";
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            var Staffs = new List<Staff>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT IdStaff, Nombre, Puesto FROM Staff";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Staff = new Staff
                            {
                                IdStaff = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Puesto = reader.GetString(2)
                            }; 

                            Staffs.Add(Staff);
                        }
                    }
                }
            }
            return Staffs;
        }

        public async Task<Staff> GetByIdAsync(int id)
        {
            Staff staff = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdStaff, Nombre, Puesto FROM Staff WHERE IdStaff = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            staff = new Staff
                            {
                                IdStaff = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Puesto = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return staff;
        }

        public async Task AddAsync(Staff staff)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Staff (Nombre, Puesto) VALUES (@Nombre, @Puesto)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", staff.Nombre);
                    command.Parameters.AddWithValue("@Puesto", staff.Puesto);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Staff staff)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Staff SET Nombre = @Nombre, Puesto = @Puesto, Equipo = @Equipo WHERE IdStaff = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", staff.Nombre);
                    command.Parameters.AddWithValue("@Puesto", staff.Puesto);
                    command.Parameters.AddWithValue("@Id", staff.IdStaff);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(int idStaff)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Staff WHERE IdStaff = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", idStaff);

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
                    INSERT INTO Staff (Nombre, Puesto)
                    VALUES 
                    (@Nombre1, @Puesto1),
                    (@Nombre2, @Puesto2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "LLeyda");
                    command.Parameters.AddWithValue("@Puesto1", "Entrenador");

                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "Mario");
                    command.Parameters.AddWithValue("@Puesto2", "Presidente");


                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}