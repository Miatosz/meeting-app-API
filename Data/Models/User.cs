using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeetingAppAPI.Data.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string EmailAdress { get; set; }
        public string Location { get; set; }

        public ICollection<Event> ListOfParticipatingEvents {get; set;}

        [JsonIgnore]
        public string Password { get; set; }


        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
