using AutoMapper;
using Storage.BLL.Requests.Users;
using Storage.BLL.Responses.Users;
using Storage.Common.Models.DTOs.User;

namespace Storage.Mapping.WebAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SearchUsersDTO, SearchUsersRequest>();
        CreateMap<UserResponse, UserDTO>();
    }
}