using AutoMapper;
using SiMinor7.Application.Users.Models;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Users;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDetailDto>();
        CreateMap<ApplicationUser, UserListDto>();
    }
}