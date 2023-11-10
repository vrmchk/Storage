using AutoMapper;
using Storage.BLL.Requests.Auth;
using Storage.BLL.Responses.Auth;
using Storage.Common.Models.DTOs.Auth;

namespace Storage.Mapping.WebAPI.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<SignUpDTO, SignUpRequest>();
        CreateMap<SignInDTO, SignInRequest>();
        CreateMap<RefreshTokenDTO, RefreshTokenRequest>();
        CreateMap<AuthSuccessResponse, AuthSuccessDTO>();
    }
}