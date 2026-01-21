using System.Security.Claims;
using RestauranteAPI.Models;

namespace RestauranteAPI.Services
{
    public interface IAuthService
    {
        public string Login(LoginDtoIn userDtoIn);
        public string Register(UserDtoIn userDtoIn);
        public string GenerateToken(UserDtoOut userDtoOut);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);


    }
}
