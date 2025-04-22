using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                Name = registrationRequestDto.Name,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result =await _userManager.CreateAsync(user,registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    var userReturn = _db.ApplicationUsers.First(u=>u.UserName == registrationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        ID = userReturn.Id,
                        Name = userReturn.Name,
                        Email = userReturn.Email,
                        PhoneNumber = userReturn.PhoneNumber

                    };
                    return userDto;
                }
            }
            catch (Exception ex)
            {

            }
            return new UserDto();
        }
       public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
       {
         throw new NotImplementedException();
       }
    }
}
