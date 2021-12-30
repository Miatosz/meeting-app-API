using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;

namespace MeetingAppAPI.Data.Models.Services
{
    public class CommentEventService : ICommentEventService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IEventRepo _eventRepo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CommentEventService(ICommentRepo cr, IEventRepo er, IMapper mapper, AppDbContext ctx)
        {
            _commentRepo = cr;
            _eventRepo = er;
            _mapper = mapper;
            _context = ctx;
        }

        public void AddCommentToEvent(int eventId, CommentDto comment)
        {
            var evt = new Event();

            // try
            // {
                evt = _eventRepo.GetEventById(eventId);
            // }
            // catch (Exception e)
            // {
            //     throw(e);
            // }
            
            
            var convertedComment = _mapper.Map<Comment>(comment);
            convertedComment.CreatedAt = DateTime.Now;

            convertedComment.CreatedBy = new User() { UserName = "test"};
            convertedComment.EventId = eventId;
              

            evt.Comments = new List<Comment>();
            evt.Comments.Add(convertedComment);

            
            

        }
    }
}
