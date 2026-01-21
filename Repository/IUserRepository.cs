using RestauranteAPI.Models;

namespace RestauranteAPI.Repositories
{
    public interface IUserRepository
    {
        public void Add(UserDtoIn user);
        public IEnumerable<UserDtoOut> GetAll();
        public UserDtoOut Get(int id);
        public void Update(UserDtoIn user);
        public void Delete(int id); 
        public UserDtoOut AddUserFromCredentials(UserDtoIn userDtoIn);
        public UserDtoOut GetUserFromCredentials(LoginDtoIn loginDtoIn);
    }
}
