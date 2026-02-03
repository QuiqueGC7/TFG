using System.Security.Claims;
using TFG.Models;

namespace TFG.Services
{
    public interface IAuthService
    {
        public string Login(LoginDtoIn userDtoIn);
        public string Register(UserDtoIn userDtoIn);
        public string GenerateToken(UserDtoOut userDtoOut);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);


    }
}
