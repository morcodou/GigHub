using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userid)
        {
            return _context
                .Attendances
                .Where(x => x.AttendeeId == userid && x.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendance(int gigId, string userid)
        {
            return _context
                .Attendances
                .SingleOrDefault(x => x.GigId == gigId && x.AttendeeId == userid);
        }
    }
}