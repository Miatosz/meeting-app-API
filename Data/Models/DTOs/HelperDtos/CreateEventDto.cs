using System;

namespace MeetingAppAPI.Data.Models.DTOs.ModelsDtos
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string Localization { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        
    }
}
