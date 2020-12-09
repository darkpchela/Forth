using AutoMapper;
using ForthSimple.Models;
using ForthSimple.ViewModels;

namespace ForthSimple.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserVM, User>().ReverseMap();
            CreateMap<UserSignUpVM, User>();
            CreateMap<UserSignUpVM, UserSignInVM>();
        }
    }
}