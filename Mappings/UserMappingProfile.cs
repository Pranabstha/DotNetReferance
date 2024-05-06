using AutoMapper;
using Authentication.Dtos;
using Authentication.Models;

namespace Authentication.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<UserRegistrationDto, ApplicationUser>();
        }
    }
}
