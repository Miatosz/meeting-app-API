using System;
using MeetingAppAPI.Data.Models.DTOs;
using MeetingAppAPI.Data.Models.DTOs.HelperDtos.User;
using MeetingAppAPI.Data.Models.Security;

namespace MeetingAppAPI.Data.Interfaces
{
    public interface IUserService
    {
        void JoinToEvent(JoinEventDto joinEventDto);
        void LeaveEvent(LeaveEventDto leaveEventDto);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
