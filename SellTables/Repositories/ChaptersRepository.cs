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
    public class ChaptersRepository : IRepository<Chapter>
    {
        private readonly ApplicationDbContext db;

        public ChaptersRepository(ApplicationDbContext db)
        {
            this.db = db;  
        }

        void IRepository<Chapter>.Add(Chapter item)
        {
            db.Chapters.Add(item);
        }

        ICollection<Chapter> IRepository<Chapter>.Find(Func<Chapter, bool> predicate)
        {
            return db.Chapters.Where(predicate).ToList();
        }

        Chapter IRepository<Chapter>.Get(int id)
        {
            return db.Chapters.Find(id);
        }

        ICollection<Chapter> IRepository<Chapter>.GetAll()
        {
            return db.Chapters.ToList();
        }

        bool IRepository<Chapter>.Remove(int id)
        {
            var Chapter = db.Chapters.Find(id);

            if (Chapter != null)
            {
                db.Chapters.Remove(Chapter);
                return true;
            }

            return false;
        }

        void IRepository<Chapter>.Update(Chapter item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}