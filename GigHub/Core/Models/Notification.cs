using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }
        public string OriginaVenue { get; private set; }

        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            this.DateTime = DateTime.Now;
            this.Gig = gig;
            Type = type;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string orignalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginaVenue = orignalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}