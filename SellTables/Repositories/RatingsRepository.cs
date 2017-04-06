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
    public class RatingsRepository : IRepository<Rating>
    {
        private readonly ApplicationDbContext dataBaseContext;

        public RatingsRepository(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        void IRepository<Rating>.Add(Rating item)
        {
            dataBaseContext.Rating.Add(item);
            dataBaseContext.SaveChanges();
        }

        ICollection<Rating> IRepository<Rating>.Find(Func<Rating, bool> predicate)
        {
            return dataBaseContext.Rating.Where(predicate).ToList();
        }

        Rating IRepository<Rating>.Get(int id)
        {
            return dataBaseContext.Rating.Find(id);
        }

        ICollection<Rating> IRepository<Rating>.GetAll()
        {
            return dataBaseContext.Rating.ToList();
        }

        bool IRepository<Rating>.Remove(int id)
        {
            var Rating = dataBaseContext.Rating.Find(id);

            if (Rating != null)
            {
                dataBaseContext.Rating.Remove(Rating);
                return true;
            }

            return false;
        }

        void IRepository<Rating>.Update(Rating item)
        {
            dataBaseContext.Entry(item).State = EntityState.Modified;
        }
    }
}