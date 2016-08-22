using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userid);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpCommingGigByArtist(string userid);
        IEnumerable<Gig> GetUpCommingGigByQuery(string query);

        void Remove(Gig gig);
    }
}