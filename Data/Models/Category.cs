using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingAppAPI.Data.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int MyProperty { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
