using SellTables.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SellTables.Models;
using System.Data.Entity;

namespace SellTables.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {

        private readonly ApplicationDbContext dataBaseContext;

        public CategoriesRepository(ApplicationDbContext dataBaseContext) {
            this.dataBaseContext = dataBaseContext;
        }

        void IRepository<Category>.Add(Category item)
        {
                dataBaseContext.Categories.Add(item);
                dataBaseContext.SaveChanges();
            
        }

        ICollection<Category> IRepository<Category>.Find(Func<Category, bool> predicate)
        {
            return dataBaseContext.Categories.Where(predicate).ToList();
        }

        Category IRepository<Category>.Get(int id)
        {
            return dataBaseContext.Categories.Find(id);
        }

        ICollection<Category> IRepository<Category>.GetAll()
        {
            return dataBaseContext.Categories.ToList();
        }

        bool IRepository<Category>.Remove(int id)
        {
            var Category = dataBaseContext.Categories.Find(id);

            if (Category != null)
            {
                dataBaseContext.Categories.Remove(Category);
                return true;
            }

            return false;
        }

        void IRepository<Category>.Update(Category item)
        {
            dataBaseContext.Entry(item).State = EntityState.Modified;
        }
    }
}