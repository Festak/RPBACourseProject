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

        public ChaptersRepository()
        {
            db = new ApplicationDbContext();
        }

        void IRepository<Chapter>.Add(Chapter item)
        {
            db.Chapters.Add(item);
        }

        ICollection<Chapter> IRepository<Chapter>.Find(Func<Chapter, bool> predicate)
        {
            return db.Chapters.Where(predicate).ToList();
        }

        async Task<Chapter> IRepository<Chapter>.Get(int id)
        {
            return await db.Chapters.FindAsync(id);
        }

        ICollection<Chapter> IRepository<Chapter>.GetAll()
        {
            return db.Chapters.ToList();
        }

        async Task<bool> IRepository<Chapter>.Remove(int id)
        {
            var Chapter = await db.Chapters.FindAsync(id);

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