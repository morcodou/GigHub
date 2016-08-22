using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index(string query = null)
        {
            var upcominggigs = _unitOfWork.Gigs.GetUpCommingGigByQuery(query);

            var userid = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userid).ToLookup(a=> a.GigId);

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