﻿using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userid);

        Attendance GetAttendance(int gigId);

        IEnumerable<Attendance> GetFutureAttendances(string userid);

        void Add(Attendance attendance);

        void Remove(Attendance attendance);
    }
}