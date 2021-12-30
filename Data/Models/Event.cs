using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingAppAPI.Data.Models
{
    public class Event
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string Localization { get; set; }
        public User CreatedBy { get; set; }
        public DateTime  CreatedAt { get; set; }
        public ICollection<User> Users { get; set; }

        public ICollection<Comment> Comments { get; set; }

        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }



    }
}
