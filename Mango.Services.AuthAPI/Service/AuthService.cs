using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        public Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
           throw new NotImplementedException();
        }
       public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
       {
         throw new NotImplementedException();
       }
    }
}
