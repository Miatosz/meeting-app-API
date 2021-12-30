using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingAppAPI.Data.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string content { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }


        // [ForeignKey("User")]
        // public int UserId { get; set; }
        // public User User { get; set; }


        [ForeignKey("Event")]
        public int EventId{ get; set; }
        public Event Events { get; set; }
    }
}
