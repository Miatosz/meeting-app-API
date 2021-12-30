using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using MeetingAppAPI.Data.Models.DTOs;
using MeetingAppAPI.Data.Models.DTOs.HelperDtos.User;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;
using MeetingAppAPI.Data.Models.Security;
using MeetingAppAPI.Data.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly IEventRepo _eventRepo;
        private readonly IUserRepo _userRepo;

        public UsersController(IUserRepo repo, IUserService userService, IMapper map, IEventRepo eventrepo, IUserRepo userrepo)
        {
            _userService = userService;
            _repo = repo;
            _mapper = map;
            _eventRepo = eventrepo;
            _userRepo = userrepo;
        }

        // [Authorize(Policy = "Admin")]
        // [Authorize]
        //[AllowAnonymous]
        [HttpGet]
        [ActionName("allUsers")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
            => Ok(_repo.Users);

        // [Authorize(Policy = "Admin")]
        [HttpGet("{name}")]
        [ActionName("getUserByName")] //<User>
        public ActionResult<UserDto> GetUserByName(string name)
        {
            
            var user = _repo.GetUserByName(name);
            
            if (user != null)
            {                
                return Ok(_mapper.Map<UserDto>(user));
            }
            else 
            {
                return NotFound("User with given name does not exists");
            }
            
        }

        // [Authorize]
        [HttpGet("{id}")]
        [ActionName("getUserById")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _repo.GetUserById(id);
            
            if (user != null)
            {
                return Ok(user);
            }
            else 
            {
                return NotFound("User with given id does not exists");
            }
            
        }
        
        // //* TODO: create user allowanonymoues on login controller
        // [HttpPost]
        // [ActionName("createUser")]
        // public ActionResult<User> CreateUser([FromBody] RegisterDto user)
        // {
        //     var convertedUser = _mapper.Map<User>(user);   
        //     _repo.CreateUser(convertedUser);
        //     _repo.SaveChanges();

        //     return CreatedAtRoute("getUserById", routeValues: new { id = convertedUser.ID }, value: convertedUser);
        // }


        [HttpDelete("{id}")]
        [ActionName("deleteUser")]
        public ActionResult DeleteUser(int id)
        {
            var user = _repo.GetUserById(id);

            if (user != null)
            {
                _repo.deleteUser(id);
                _repo.SaveChanges();
                
                return Ok();
            }
            else
            {
                return NotFound("User with given id does not exists");
            }
        }

        [HttpGet("{id}")]
        [ActionName("getUserEvents")]
        public ActionResult<List<EventDto>> GetUserEvents(int id)
        {
            var user = _userRepo.GetUserById(id);

            if (user != null)
            {
                var events = user.ListOfParticipatingEvents;                
                return _mapper.Map<List<EventDto>>(events).ToList();
            }
            else if (user == null)
            {
                return NotFound();
            }
            return BadRequest();

        }

        [HttpPost]
        [ActionName("joinToEvent")]
        public ActionResult JoinToEvent([FromBody] JoinEventDto joinEventDto)
        {
            Console.WriteLine(joinEventDto.UserId +  "  " + joinEventDto.EventId);            

            _userService.JoinToEvent(joinEventDto);           

            return Ok();
        }

        [HttpDelete("{userId}/eventId")]
        [ActionName("leaveEvent")]
        public ActionResult LeaveEvent([FromRoute] int userId, int eventId)
        {
            var dto = new LeaveEventDto { UserId = userId, EventId = eventId};
            _userService.LeaveEvent(dto);
        
            return Ok();
        }

        [HttpPut]
        [ActionName("updateUser")]
        public ActionResult<User> UpdateUser([FromBody] User user)
        {
            var updateResult = _repo.UpdateUser(user);

            if (updateResult == true)
            {
                _repo.SaveChanges();
                return CreatedAtRoute("getUserById", routeValues: new { id = user.ID }, value: user);
            }
            else
            {
                return NotFound("this user does not exists");
            }

        }


        // [HttpPost]
        // [ActionName("authenticate")]
        // public ActionResult Authenticate(AuthenticateRequest model)
        // {
        //     Console.WriteLine("authentication...");
        //     var response = _userService.Authenticate(model);

        //     if (response == null)
        //         return BadRequest(new { message = "Username or password is incorrect" });

        //     return Ok(response);
        // }
    }
}
