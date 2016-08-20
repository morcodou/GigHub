using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userid);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpCommingGigdByArtist(string userid);
    }
}