using System.Data.SqlClient;
using Models;
using TFG.Models;

namespace TFG.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Not found";
        }

        public void Add(UserDtoIn user)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public IEnumerable<UserDtoOut> GetAll()
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public UserDtoOut Get(int id)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public void Update(UserDtoIn user)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public void SaveChanges()
        {
            throw new NotImplementedException("Not implemented yet");
        }

        // 🔐 REGISTRO (si algún día lo usas)
        public UserDtoOut AddUserFromCredentials(UserDtoIn userDtoIn)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    INSERT INTO Users (UserName, Email, PasswordHash, Role)
                    OUTPUT INSERTED.UserId
                    VALUES (@UserName, @Email, @PasswordHash, @Role)
                ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userDtoIn.UserName);
                    command.Parameters.AddWithValue("@Email", userDtoIn.Email);
                    command.Parameters.AddWithValue("@PasswordHash", userDtoIn.Password);
                    command.Parameters.AddWithValue("@Role", userDtoIn.Role);

                    var newId = (int)command.ExecuteScalar();

                    return new UserDtoOut
                    {
                        UserId = newId,
                        UserName = userDtoIn.UserName,
                        Email = userDtoIn.Email,
                        Role = userDtoIn.Role
                    };
                }
            }
        }

        // 🔐 LOGIN REAL
        public UserDtoOut GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT UserId, UserName, Email, PasswordHash, Role
                    FROM Users
                    WHERE UserName = @UserName AND PasswordHash = @Password
                ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", loginDtoIn.UserName);
                    command.Parameters.AddWithValue("@Password", loginDtoIn.Password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new KeyNotFoundException("User not found.");
                        }

                        return new UserDtoOut
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Email = reader.GetString(2),
                            Role = reader.GetString(4)
                        };
                    }
                }
            }
        }
    }
}
