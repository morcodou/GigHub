using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcomingsGigs { get; set; }
        public bool ShowActions { get; set; }

    }
}