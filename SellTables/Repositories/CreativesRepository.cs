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

        public ICollection<Creative> GetRange(int start, int count, int sortType)
        {
            using (var dbc = new ApplicationDbContext())
            {
                IEnumerable<Creative> result = null;
                if (sortType == 1) {
                    result = GetDateSortedCreatives(dbc);
                }
                if (sortType == 2) {
                    result = GetEditDateSortedCreatives(dbc);
                }
                if (sortType == 3) {
                    result = GetNameSortedCreatives(dbc);
                }
                else {
                    result = GetDateSortedCreatives(dbc);
                }
                
                
                return result.Skip(start - 1).Take(count).ToList();
            }
        }

        private IEnumerable<Creative> GetDateSortedCreatives(ApplicationDbContext dbc) {
            return dbc.Creatives.Include(c => c.Chapters).OrderByDescending(c => c.CreationDate);
        }

        private IEnumerable<Creative> GetEditDateSortedCreatives(ApplicationDbContext dbc)
        {
            return dbc.Creatives.Include(c => c.Chapters).OrderBy(c => c.Id);
        }

        private IEnumerable<Creative> GetNameSortedCreatives(ApplicationDbContext dbc)
        {
            return dbc.Creatives.Include(c => c.Chapters).OrderBy(c => c.Name);
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