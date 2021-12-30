using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Models;

namespace MeetingAppAPI.Data.Interfaces
{
    public interface ICommentRepo
    {
        IEnumerable<Comment> GetAllCommentsByEvent(int id);
        IEnumerable<Comment> GetAllCommentsByUser(int id);
        void CreateComment(Comment comment);
        bool DeleteCommentById(int id);
        public bool SaveChanges();

    }
}
