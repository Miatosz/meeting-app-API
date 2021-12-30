using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingAppAPI.Data.Models;

namespace MeetingAppAPI.Data.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}