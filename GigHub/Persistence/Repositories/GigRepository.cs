﻿using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;
        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userid)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userid)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                 .Include(g => g.Attendances.Select(a => a.Attendee))
                 .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        ////public Gig GetGigWithArtistsAndGenres(int gigId)
        ////{
        ////    return _context.Gigs
        ////        .Include(g => g.Artist)
        ////        .Include(g => g.Genre)
        ////        .SingleOrDefault(g => g.Id == gigId);
        ////}

        public IEnumerable<Gig> GetUpCommingGigByArtist(string userid)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userid && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpCommingGigByQuery(string query)
        {
            var upcominggigs = _context.Gigs
                                .Include(g => g.Artist)
                                .Include(g => g.Genre)
                                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcominggigs = upcominggigs.Where(g =>
                                            g.Artist.Name.Contains(query) ||
                                            g.Genre.Name.Contains(query) ||
                                            g.Venue.Contains(query));
            }

            return upcominggigs;
        }

        public void Remove(Gig gig)
        {
            gig.Cancel();
        }
    }
}