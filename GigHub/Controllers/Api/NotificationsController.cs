using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userid = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userid && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            ////var config = new MapperConfiguration(cfg =>
            ////{
            ////    cfg.CreateMap<ApplicationUser, UserDto>();
            ////    cfg.CreateMap<Gig, GigDto>();
            ////    cfg.CreateMap<Notification, NotificationDto>();
            ////});
            ////var mapper = config.CreateMapper();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
