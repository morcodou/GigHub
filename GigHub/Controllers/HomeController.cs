using GigHub.Core.ViewModels;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _context;
        public readonly AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            var upcominggigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcominggigs = upcominggigs
                    .Where(g =>
                            g.Artist.Name.Contains(query) ||
                            g.Genre.Name.Contains(query) ||
                            g.Venue.Contains(query)
                    );
            }

            var userid = User.Identity.GetUserId();
            var attendances = _attendanceRepository.GetFutureAttendances(userid).ToLookup(a=> a.GigId);

            var viewmodel = new GigsViewModel()
            {
                UpcomingsGigs = upcominggigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcomings Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewmodel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}