using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; internal set; }
        public bool IsFollowing { get; set; }
        public bool IsAttending { get; set; }
    }
}