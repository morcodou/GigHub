using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class Following
    {
        public string FollowerId { get; set; }

        public string FolloweeId { get; set; }

        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }

    }
}