using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingAppAPI.Data.Models.DTOs.ModelsDtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
