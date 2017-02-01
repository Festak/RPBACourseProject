using SellTables.Interfaces;
using SellTables.Models;
using SellTables.Repositories;
using SellTables.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellTables.Services
{
    public class CategoryService 
    {

        private IRepository<Category> Repository;
        private IUserRepository UsersRepository;
        private ApplicationDbContext dataBaseContext;

        public CategoryService(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            Repository = new CategoriesRepository(dataBaseContext);
            UsersRepository = new UsersRepository(dataBaseContext);
        }

        public List<Category> GetAllCategories()
        {
            var listOfCategories = Repository.GetAll().ToList();
            return listOfCategories;
        }

        public List<CategorySubModel> GetCategoriesWithSbs(string userId) {
            var listOffCategories = Repository.GetAll().ToList();
            var listOfSubCategories = InitModel(listOffCategories, userId);
            return listOfSubCategories;
        }

        private bool IsUserSubscibed(string userId, Category category)
        {
            var categories = dataBaseContext.Subscribes.Where(u => u.UserId == userId && u.Category.Id == category.Id).ToList();
            if (categories.Count == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        private List<CategorySubModel> InitModel(List<Category> categories, string userId) {
      
            if (categories != null)
            {
                return categories.Select(category => new CategorySubModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    isSubscribed = IsUserSubscibed(userId, category)
                }).ToList();
            }
            else
            {
                return new List<CategorySubModel>();
            }
        }

        public void SubscribeToCategory(int id, string userId) {
            Subscribe sub = new Subscribe();
            ApplicationUser user = UsersRepository.FindUserById(userId);
            sub.UserId = userId;
            sub.CategoryId = id;
            sub.UserEmail = user.Email;
            dataBaseContext.Subscribes.Add(sub);
            dataBaseContext.SaveChanges();
        }

        public void UnSubscribeFromCategory(int id, string userId) {
            Subscribe sub = dataBaseContext.Subscribes.FirstOrDefault(u=>u.UserId == userId && u.CategoryId == id);     
            dataBaseContext.Subscribes.Remove(sub);
            dataBaseContext.SaveChanges();
        }


    }
}