using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models.DTOs;
using MeetingAppAPI.Data.Models.DTOs.HelperDtos.User;
using MeetingAppAPI.Data.Models.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetingAppAPI.Data.Models.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IEventRepo _eventRepo;
        private readonly AppSettings _appSettings;
        

        public UserService(IUserRepo userRepo, IEventRepo eventRepo, IOptions<AppSettings> appSettings)
        {
            _userRepo = userRepo;
            _eventRepo = eventRepo;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userRepo.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public void JoinToEvent(JoinEventDto joinEventDto)
        {
            var user = _userRepo.GetUserById(joinEventDto.UserId);
            var evt = _eventRepo.GetEventById(joinEventDto.EventId);

            if (evt.Users == null)
            {
                evt.Users = new List<User>();
            }
           

           _eventRepo.GetEventById(evt.ID).Users.Add(user);

            _eventRepo.SaveChanges();
        }

        public void LeaveEvent(LeaveEventDto leaveEventDto)
        {
            var user = _userRepo.GetUserById(leaveEventDto.UserId);

            _eventRepo.GetEventById(leaveEventDto.EventId).Users.Remove(user);

            _eventRepo.SaveChanges();
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
