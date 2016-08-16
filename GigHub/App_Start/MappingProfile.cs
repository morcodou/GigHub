using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
        }
    }
}