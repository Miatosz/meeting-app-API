using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAppAPI.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;


        public UserRepo(AppDbContext ctx)
        {
            _context = ctx;
        }


        public IEnumerable<User> Users
            => _context.Users.Include(x => x.ListOfParticipatingEvents);


        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public bool deleteUser(int id)
        {
            var user = _context.Users.First(x => x.ID == id);

            if(user != null)
            {
                _context.Users.Remove(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserByName(string name)
        {
            return _context.Users.First(x => x.UserName == name);
        }

        public bool UpdateUser(User usr)
        {
            var user = _context.Users.First(x => x.ID == usr.ID);
            
            if (user != null)
            {
                user.UserName = usr.UserName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(c => c.ListOfParticipatingEvents).First(x => x.ID == id);
        }
    }
}
