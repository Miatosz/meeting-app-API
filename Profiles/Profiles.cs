using System;
using System.Collections.Generic;
using AutoMapper;
using MeetingAppAPI.Data.Models;
using MeetingAppAPI.Data.Models.DTOs;
using MeetingAppAPI.Data.Models.DTOs.HelperDtos;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;

namespace MeetingAppAPI.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, UserInEventDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<EventCreatedByUser, User>();
            CreateMap<EventDto, Event>();
            CreateMap<CreateEventDto, Event>();
            CreateMap<EventInCategoryListDto, Event>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

        }
    }
}
