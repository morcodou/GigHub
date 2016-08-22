using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    internal class NotificationRepository : INotificationRepository
    {
        private ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Notification> GetNewNotifications(string userid)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userid && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }
    }
}