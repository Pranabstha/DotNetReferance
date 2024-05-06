using Authentication.Dtos;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userForRegistration);
        Task<bool> ValidateUserAsync(UserLoginDto loginDto);
        Task<string> GenerateTokenAsync();
        public string GetUserId();
    }
} 
