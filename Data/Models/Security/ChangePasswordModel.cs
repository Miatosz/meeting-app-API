using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAppAPI.Data.Models.Security
{
    public class ChangePasswordModel
    {
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Email { get; set; }
        
    }
}