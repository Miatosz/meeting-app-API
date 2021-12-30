using System;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;

namespace MeetingAppAPI.Data.Interfaces
{
    public interface ICommentEventService
    {
        void AddCommentToEvent(int eventId, CommentDto comment);
    }
}
