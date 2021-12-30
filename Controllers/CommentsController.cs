using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MeetingAppAPI.Data;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICommentRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommentEventService _commentUserService;

        public CommentsController(AppDbContext ctx, ICommentRepo repo, IMapper mapper, ICommentEventService srv)
        {
            _context = ctx;
            _repo = repo;
            _mapper = mapper;
            _commentUserService = srv;
        }


        [HttpGet("{id}")]
        [ActionName("getAllByEvent")]
        public ActionResult<List<CommentDto>> GetAllCommentsByEvent(int id)
        {
            var comments = _repo.GetAllCommentsByEvent(id).ToList();

            comments.ForEach(x => x.CreatedBy = _context.Users.SingleOrDefault(c => c.ID.Equals(x.CreatedBy)));

            return _mapper.Map<List<CommentDto>>(comments);
        }

        [HttpGet("{id}")]
        [ActionName("getAllByUser")]
        public ActionResult<List<CommentDto>> GetAllCommentsByUser( [FromBody] int id)
        {
            var comments = new List<Comment>();
            try 
            {
                comments.AddRange(_repo.GetAllCommentsByUser(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return _mapper.Map<List<CommentDto>>(comments);
        }


        [HttpPost("{id}")]
        [ActionName("AddComment")]
        public ActionResult AddComment([FromBody] CommentDto comment, [FromRoute] int id)
        {
            _commentUserService.AddCommentToEvent(id,comment);
            _repo.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteComment")]
        public ActionResult DeleteComment(int id)
        {
            if (_repo.DeleteCommentById(id) == true)
            {
                _repo.SaveChanges();
                return Ok();
            }
            else 
            {
                return NotFound();
            }
           
        }
    }
}
