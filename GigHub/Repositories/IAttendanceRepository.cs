using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userid);
        IEnumerable<Attendance> GetFutureAttendances(string userid);
    }
}