using AutoMapper;
using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;
using EndProject.Core.Entities;

namespace EndProject.Business.MappingProfiles;

public class UserAboutMapProfile : Profile
{
    public UserAboutMapProfile()
    {
        CreateMap<UserAboutPutDTO, UserAbout>().ReverseMap();
        CreateMap<UserAboutGetDTO, UserAbout>().ReverseMap();
    }
}
