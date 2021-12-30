using System;
using System.Collections.Generic;

namespace MeetingAppAPI.Data.Models.DTOs.ModelsDtos
{
    public class CategoryDto
    {
        public string Name { get; set; }
       //public int MyProperty { get; set; }

        public ICollection<EventInCategoryListDto> Events { get; set; }
        public string ImageUrl { get; set; }

    
    }
}
