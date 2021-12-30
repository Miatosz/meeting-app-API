using System;
using System.Collections.Generic;
using MeetingAppAPI.Data.Models;

namespace MeetingAppAPI.Data.Interfaces
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAllCategories();
        void CreateCategory(Category category);
        Category GetCategoryByName(string name);
        public bool SaveChanges();
        public bool DeleteCategory(int id);
        public IEnumerable<Category> GetCategories {get;}
        public IEnumerable<Category> GetCategoriesWithEvents {get;}
    }
}
