using AutoMapper;
using Storage.BLL.Requests.Auth;
using Storage.BLL.Responses.Users;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpRequest, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UserName));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.DisplayName));
    }
}