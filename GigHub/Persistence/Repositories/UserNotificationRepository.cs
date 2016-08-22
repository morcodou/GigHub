using GigHub.Core.Repositories;
using GigHub.Models;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    internal class UserNotificationRepository : IUserNotificationRepository
    {
        private ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void ChangeNotificationsAsRead(string userid)
        {
            var notifications = _context.UserNotifications.Where(un => un.UserId == userid && !un.IsRead).ToList();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
        }
    }
}