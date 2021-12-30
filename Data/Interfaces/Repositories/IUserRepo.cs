using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Models;

namespace MeetingAppAPI.Data.Interfaces
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByName(string name);
        User GetUserById(int id);
        void CreateUser(User user);
        bool deleteUser(int id);
        bool UpdateUser(User user);
        public bool SaveChanges();
        IEnumerable<User> Users {get;}


    }
}
