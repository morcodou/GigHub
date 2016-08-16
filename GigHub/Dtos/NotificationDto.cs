using GigHub.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get;  set; }
        public NotificationType Type { get;  set; }

        public DateTime? OriginalDateTime { get;  set; }
        public string OriginaVenue { get;  set; }

        public GigDto Gig { get;  set; }
    }
}