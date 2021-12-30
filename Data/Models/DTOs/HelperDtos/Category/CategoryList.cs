using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;

namespace MeetingAppAPI.Data.Models.DTOs.HelperDtos
{
    public class CategoryList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EventInCategoryListDto> Events { get; set; }
        public string ImageUrl { get; set; }
    }
}
