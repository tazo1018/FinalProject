using AutoMapper;
using FinalProject.Entities;
using FinalProject.Models.Comment;
using FinalProject.Models.Identity;
using FinalProject.Models.Post;
using FinalProject.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Service
{
    internal class MappingInitializer
    {
        
        public static IMapper Initialize()
        {
            MapperConfiguration configuration = new(config =>
            {
                config.CreateMap<Post, PostForCreatingDTO>().ReverseMap();
                config.CreateMap<Post, PostForUpdatingDTO>().ReverseMap();
                config.CreateMap<Post, PostForGettingDTO>().ReverseMap();
                config.CreateMap<Post, PostForGettingDetailedDTO>().ReverseMap();

                config.CreateMap<Comment, CommentForCreatingDTO>().ReverseMap();
                config.CreateMap<Comment, CommentForUpdatingDTO>().ReverseMap();
                config.CreateMap<Comment, CommentForGettingDTO>().ReverseMap();

                config.CreateMap<User, UserForGettingDTO>().ReverseMap();
                config.CreateMap<User, UserForUpdatingDTO>().ReverseMap();

                
                config.CreateMap<UserDTO, IdentityUser>().ReverseMap();
                config.CreateMap<RegistrationRequestDTO, IdentityUser>()
                
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.NormalizedUserName, options => options.MapFrom(source => source.Email.ToUpper()))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.NormalizedEmail, options => options.MapFrom(source => source.Email.ToUpper()))
                .ForMember(destination => destination.PhoneNumber, options => options.MapFrom(source => source.PhoneNUmber));
            });

            return configuration.CreateMapper();
        }
    }
}
