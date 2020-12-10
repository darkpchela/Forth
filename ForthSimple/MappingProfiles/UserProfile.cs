using AutoMapper;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ForthSimple.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserVM, IdentityUser>().ReverseMap();
            CreateMap<UserSignUpVM, IdentityUser>();
            CreateMap<UserSignUpVM, UserSignInVM>();
        }
    }
}