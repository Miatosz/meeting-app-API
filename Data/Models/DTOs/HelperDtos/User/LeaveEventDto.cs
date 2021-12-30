using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAppAPI.Data.Models.DTOs.HelperDtos.User
{
    public class LeaveEventDto
    {
        public int UserId { get; set; }   
        public int EventId { get; set; }
    }
}