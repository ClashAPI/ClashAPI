using AutoMapper;
using backend.DTOs;
using backend.Models;

namespace backend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForLoginDto>();
            CreateMap<User, UserForListDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<CreateAnnouncementDto, Announcement>();
        }
    }
}