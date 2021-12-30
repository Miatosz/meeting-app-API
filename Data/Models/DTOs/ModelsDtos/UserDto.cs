using System;
using System.Collections.Generic;

namespace MeetingAppAPI.Data.Models.DTOs.ModelsDtos
{
    public class UserDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }

        public ICollection<Event> ListOfParticipatingEvents {get; set;}
    }
}
