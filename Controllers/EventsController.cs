using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MeetingAppAPI.Data;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using MeetingAppAPI.Data.Models.DTOs;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetingAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]/")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEventRepo _repo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;


        public EventsController(IEventRepo repo, AppDbContext ctx, IMapper mapper, IUserRepo userrepo)
        {
            _mapper = mapper;
            _context = ctx;
            _repo = repo;
            _userRepo = userrepo;
        }

        // GET: api/events
        [HttpGet]
        [ActionName("all")]
        public ActionResult<IEnumerable<EventDto>> GetEvents()
        {           
            var events = _context.Events.Include(x => x.Users).ToList();

            try 
            {
                var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events).ToList();

                eventsDto.ForEach(x => x.Users = new List<UserInEventDto>(
                _mapper.Map<IEnumerable<UserInEventDto>>(x.Users)));

                return Ok(eventsDto);
            
            }
            catch (AutoMapperMappingException ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex);
                Console.ResetColor();
                
                return BadRequest();
            }
        }

      
        // GET: api/event/{name}
        [HttpGet("{name}", Name = "GetEventByName")]
        [ActionName("GetEventByName")]
        public ActionResult<EventDto> GetEventByName(string name)
        {
            var evt = _repo.GetEventByName(name);

            if (evt != null)
            {
                return Ok(MapEvent(evt));
            }
            else
            {
                return NotFound("Event with given name doesnt exist");
            }
        }

        [HttpGet("{id}")]
        [ActionName("getEventsForCategory")]
        public ActionResult<List<EventDto>> GetEventsForCategory(int id)
        {
            var events = _repo.GetEventsByCategory(id);
            if (events.Count() > 0)
                return Ok(events);
            return NotFound();
        }


        // GET: api/event/{id}
        [HttpGet("{id}", Name = "GetEventById")]
        [ActionName("GetEventById")]
        public ActionResult<EventDto> GetEventById(int id)
        {
            var evt = _repo.GetAllEvents().First(x => x.ID == id);
            evt.Users = _userRepo.Users.Where(x => x.ListOfParticipatingEvents.Contains(evt)).ToList();


            if (evt != null)
            {              
                return Ok(MapEvent(evt));
            }
            else
            {
                return NotFound("Event with given id doesnt exist");
            }
        }

        // GET: api/event/{date}
        [HttpGet("{date}", Name = "GetEventByDate")]
        [ActionName("GetEventByDate")]
        public ActionResult<Event> GetEventByDate(DateTime date)
        {
            var evt = _repo.GetEventByDate(date);

            if (evt != null)
            {
                return Ok(evt);
            }
            else
            {
                return NotFound("Event with given date doesnt exist");
            }
        }


        // GET: api/event/{localisation}
        [HttpGet("{localisation}", Name = "GetEventByLocalisation")]
        [ActionName("GetEventByLocalisation")]
        public ActionResult<Event> GetEventByLocalisation(string localisation)
        {
            var evt = _repo.GetEventByLocalisation(localisation);

            if (evt != null)
            {
                return Ok(MapEvent(evt));
            }
            else
            {
                return NotFound("Event with given localisation doesnt exist");
            }
        }


        // POST: api/events/createEvent
        [HttpPost]
        [ActionName("createEvent")]
        public ActionResult<Event> CreateEvent([FromBody] CreateEventDto evt)
        {
            var mappedEvent = _mapper.Map<Event>(evt);

            _repo.CreateEvent(mappedEvent);
            _repo.SaveChanges();

            return CreatedAtRoute("GetEventById", routeValues: new { id = mappedEvent.ID }, value: mappedEvent);
        }


        // Delete: api/events/deleteEvents/{id}
        [HttpDelete("{id}")]
        [ActionName("deleteEvent")]
        public ActionResult DeleteEvent(int id)
        {
            var evt = _repo.DeleteEvent(id);
            _repo.SaveChanges();

            if (evt == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }    
    
        private EventDto MapEvent(Event evt)
        {
            var eventDto = _mapper.Map<EventDto>(evt);
               
            var users = _mapper.Map<List<UserInEventDto>>(_context.Users.Where(x 
                                                                            => x.ListOfParticipatingEvents.Any(x 
                                                                                => x.ID == evt.ID)));
                                                                                    
            var comments = _mapper.Map<List<CommentDto>>(_context.Comments.Where(x => x.EventId == evt.ID));

            var crby = evt.CreatedBy;
            var createdBy = _mapper.Map<EventCreatedByUser>(crby);

            eventDto.CreatedBy = createdBy;
            eventDto.Users = users;
            eventDto.Comments = comments;

            return eventDto;
        }
    
    
    
    
    
    }
}
