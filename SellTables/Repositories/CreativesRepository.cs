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
    public class CreativesRepository : IRepository<Creative>
    {
        private readonly ApplicationDbContext db;

        public CreativesRepository()
        {
            db = new ApplicationDbContext();
        }

        void IRepository<Creative>.Add(Creative item)
        {
            db.Creatives.Add(item);
        }

        ICollection<Creative> IRepository<Creative>.Find(Func<Creative, bool> predicate)
        {
            return db.Creatives.Where(predicate).ToList();
        }

        async Task<Creative> IRepository<Creative>.Get(int id)
        {
            return await db.Creatives.FindAsync(id);
        }

        ICollection<Creative> IRepository<Creative>.GetAll()
        {
            return db.Creatives.Include(c => c.Chapters).ToList();
        }

        public ICollection<Creative> GetRange(int start, int count)
        {
            using (var dbc = new ApplicationDbContext())
            {
                if (!(dbc.Creatives.Include(c => c.Chapters).Where(c => c.Id >= start && c.Id < start + count) == null))
                {
                    return dbc.Creatives.Include(c => c.Chapters).Where(c => c.Id >= start && c.Id < start + count).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        async Task<bool> IRepository<Creative>.Remove(int id)
        {
            var Creative = await db.Creatives.FindAsync(id);

            if (Creative != null)
            {
                db.Creatives.Remove(Creative);
                return true;
            }

            return false;
        }

        void IRepository<Creative>.Update(Creative item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}