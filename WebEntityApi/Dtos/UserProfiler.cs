using AutoMapper;
using WebEntityApi.Models;

namespace WebEntityApi.Dtos;

public class UserProfiler : Profile
{
    public UserProfiler()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
