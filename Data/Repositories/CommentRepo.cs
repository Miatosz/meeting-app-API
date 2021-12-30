using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using System.Linq;
namespace MeetingAppAPI.Data.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _context;

        public CommentRepo(AppDbContext ctx)
        {
            _context = ctx;
        }


        public void CreateComment(Comment comment)
        {
            _context.Add(comment);
        }


        public bool DeleteCommentById(int id)
        {
            var comment = _context.Comments.First(x => x.ID == id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                return true;
            }
            else
            {
                return false;
            }
        }


        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments;
        }

        public IEnumerable<Comment> GetAllCommentsByEvent(int id)
        {
            return _context.Comments.Where(x => x.EventId == id);
        }

        public IEnumerable<Comment> GetAllCommentsByUser(int id)
        {
            return _context.Comments.Where(x => x.CreatedBy.ID == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
