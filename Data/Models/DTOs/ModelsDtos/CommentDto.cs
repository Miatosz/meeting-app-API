using System;

namespace MeetingAppAPI.Data.Models.DTOs.ModelsDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string content { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        //public User User { get; set; }
    }
}
