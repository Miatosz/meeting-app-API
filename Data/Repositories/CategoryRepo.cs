using System;
using System.Collections.Generic;
using System.Linq;
using MeetingAppAPI.Data.Interfaces;
using MeetingAppAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAppAPI.Data.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Category> GetCategories => _context.Categories;
        public IEnumerable<Category> GetCategoriesWithEvents => _context.Categories.Include(x => x.Events);

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.First(x => x.ID == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(x => x.Name == name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
