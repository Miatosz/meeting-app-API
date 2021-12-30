using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;

namespace MeetingAppAPI.Data.Models.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string Localization { get; set; }
        public EventCreatedByUser CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public DateTime  CreatedAt { get; set; }

        public  List<UserInEventDto> Users { get; set; }
        public List<CommentDto> Comments { get; set; }

    }
}
