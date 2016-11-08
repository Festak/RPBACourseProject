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
        private readonly ApplicationDbContext db;

        public RatingsRepository()
        {
            db = new ApplicationDbContext();
        }

        void IRepository<Rating>.Add(Rating item, ApplicationDbContext db)
        {
            db.Rating.Add(item);
        }

        ICollection<Rating> IRepository<Rating>.Find(Func<Rating, bool> predicate)
        {
            return db.Rating.Where(predicate).ToList();
        }

        Rating IRepository<Rating>.Get(int id)
        {
            return db.Rating.Find(id);
        }

        ICollection<Rating> IRepository<Rating>.GetAll()
        {
            return db.Rating.ToList();
        }

        bool IRepository<Rating>.Remove(int id)
        {
            var Rating = db.Rating.Find(id);

            if (Rating != null)
            {
                db.Rating.Remove(Rating);
                return true;
            }

            return false;
        }

        void IRepository<Rating>.Update(Rating item, ApplicationDbContext db)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}