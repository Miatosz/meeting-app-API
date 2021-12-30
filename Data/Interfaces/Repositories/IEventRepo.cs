using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Models;

namespace MeetingAppAPI.Data.Interfaces
{
    public interface IEventRepo
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventByName(string name);
        Event GetEventByDate(DateTime date);
        Event GetEventByLocalisation(string localisation);
        Event GetEventById(int id);
        IEnumerable<Event> GetEventsByCategory(string category);
        IEnumerable<Event> GetEventsByCategory(int id);
        void CreateEvent(Event ev);
        void UpdateEvent(int id, Event ev);
        bool DeleteEvent(int id);
        bool SaveChanges();
        IEnumerable<Event> Events {get;}



        
    }
}
