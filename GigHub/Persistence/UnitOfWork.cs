using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Models;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IFollowingRepository Followings { get; private set; }

        public IApplicationUserRepository Users { get; private set; }

        public INotificationRepository Notifications { get; private set; }

        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Genres = new GenreRepository(_context);
            Followings = new FollowingRepository(_context);
            Users = new ApplicationUserRepository(_context);
            Notifications = new NotificationRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
       }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}