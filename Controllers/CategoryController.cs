using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AutoMapper;
using MeetingAppAPI.Data;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using MeetingAppAPI.Data.Models.DTOs;
using MeetingAppAPI.Data.Models.DTOs.HelperDtos;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext ctx, ICategoryRepo repo, IMapper mapper)
        {
            _context = ctx;
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        [ActionName("getAll")]
        public ActionResult<List<CategoryList>> GetCategories()
        {
            var categories = _repo.GetCategories;
            var events = _context.Events;
            var dto = _mapper.Map<List<CategoryList>>(categories);
            dto.ForEach(x => x.Events = new List<EventInCategoryListDto>());
            //dto.ForEach(x => x.Events.ForEach(z => z.CreatedBy = events.Where(c => c.ID == z.Id).First().CreatedBy.ID));
            //Console.WriteLine(dto.First(x => x != null).Events.First() + "XD");
            return dto;
        }

        // [HttpGet("{id}")]
        // [ActionName("getEventsForCategory")]
        // public ActionResult<List<EventDto>> GetEventsForCategory(int id)
        // {
        //     // var category = _repo.GetCategoriesWithEvents.Where(x => x.ID == id);
        //     // if (category.Count() > 0)
        //     // {
        //     //     var events = new List<EventDto>();
        //     //     events.AddRange(category.);

        //     //     return Ok(categories);
        //     // }
                
        //     // return NotFound();
        // }


        [HttpGet("{name}", Name = "GetByName")]
        [ActionName("GetByName")]
        public ActionResult<CategoryDto> GetCategoryByName( [FromRoute] string name)
        {
            var foundCategory = _repo.GetCategoryByName(name);
            if ( foundCategory != null )
            {
                return MapCategory(foundCategory);
            }
            else
            {
                return NotFound();
            }
        }

        //! [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("createCategory")]
        public ActionResult CreateCategory( [FromBody] CategoryCreateDto category)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateCategory(_mapper.Map<Category>(category));
                _repo.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ActionName("deleteCategory")]
        public ActionResult DeleteCategory( [FromRoute] int id)
        {
            try 
            {
                _repo.DeleteCategory(id);
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }


        private CategoryDto MapCategory(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);
                                                                                             
            categoryDto.Events = _mapper.Map<List<EventInCategoryListDto>>(_context.Events.Where(
                                                                            x => x.CategoryId == category.ID).ToList());

            return categoryDto;
        }

        
        
    }
}
