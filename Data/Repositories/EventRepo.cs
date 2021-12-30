using System;
using System.Collections.Generic;
using System.Linq;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAppAPI.Data.Repositories
{
    public class EventRepo : IEventRepo
    {
        private readonly AppDbContext _context;

        public EventRepo(AppDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Event> Events => _context.Events;


        public void CreateEvent(Event ev)
        {
            ev.CreatedAt = DateTime.Now;
            _context.Events.Add(ev);
        }

        public bool DeleteEvent(int id)
        {
            var evt = _context.Events.First(x => x.ID == id);

            if (evt != null)
            {
                _context.Events.Remove(evt);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events;
        }


        public Event GetEventByDate(DateTime date)
        {
            return _context.Events.First(x => x.DateOfEvent == date);
        }


        public Event GetEventByLocalisation(string lcs)
        {
            return _context.Events.First(x => x.Localization == lcs);
        }


        public Event GetEventByName(string name)
        {
            return _context.Events.First(x => x.Name == name);
        }

        public IEnumerable<Event> GetEventsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(int id, Event ev)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Event GetEventById(int id)
        {
            return _context.Events.Include(x => x.Users).First(x => x.ID == id);
        }

        public IEnumerable<Event> GetEventsByCategory(int id)
        {
            return _context.Events.Where(x => x.CategoryId == id);
        }
    }
}
