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

        public CreativesRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        void IRepository<Creative>.Add(Creative item)
        {
           
                db.Creatives.Add(item);
                db.SaveChanges();
            
        }

        ICollection<Creative> IRepository<Creative>.Find(Func<Creative, bool> predicate)
        {
            return db.Creatives.Where(predicate).ToList();
        }

        Creative IRepository<Creative>.Get(int id)
        {
            return db.Creatives.Find(id);
        }

        ICollection<Creative> IRepository<Creative>.GetAll()
        {
            return db.Creatives.Include(c => c.Chapters).Include(u => u.User).ToList();
        }
        // TODO: replace ifs
        public ICollection<Creative> GetRange(int start, int count, int sortType, ApplicationDbContext db)
        {
           
                IEnumerable<Creative> result = null;
                if (sortType == 1) {
                    result = GetDateSortedCreatives(db);
                }
                if (sortType == 2) {
                    result = GetEditDateSortedCreatives(db);
                }
                if (sortType == 3) {
                    result = GetNameSortedCreatives(db);
                }
                else {
                    result = GetDateSortedCreatives(db);
                }
                
                
                return result.Skip(start - 1).Take(count).ToList();
            }
        

        private IEnumerable<Creative> GetDateSortedCreatives(ApplicationDbContext dbc) {
            return dbc.Creatives.Include(c => c.Chapters).Include(u => u.User).OrderByDescending(c => c.CreationDate);
        }

        private IEnumerable<Creative> GetEditDateSortedCreatives(ApplicationDbContext dbc)
        {
            return dbc.Creatives.Include(c => c.Chapters).Include(u => u.User).OrderBy(c => c.Id);
        }

        private IEnumerable<Creative> GetNameSortedCreatives(ApplicationDbContext dbc)
        {
            return dbc.Creatives.Include(c => c.Chapters).Include(u => u.User).OrderBy(c => c.Name);
        }
     

        public ICollection<Creative> GetPopular()
        {
            int count = db.Creatives.Where(p => p.Rating >= 4).Count();
            if (count > 10)
            {
                return db.Creatives.Include(c => c.Chapters).Include(u => u.User).Where(p => p.Rating >= 4).Take(10).OrderBy(p => p.Rating).ToList();
            }
            else {
                return db.Creatives.Include(c => c.Chapters).Include(u => u.User).Where(p => p.Rating >= 4).Take(count).OrderBy(p => p.Rating).ToList();
            }
        }

        bool IRepository<Creative>.Remove(int id)
        {
            var Creative = db.Creatives.Find(id);

            if (Creative != null)
            {
                db.Creatives.Remove(Creative);
                return true;
            }

            return false;
        }

        void IRepository<Creative>.Update(Creative item)
        {
            if (item != null)
            {
                var _Item = db.Entry(item);
                Creative itemObj = db.Creatives.Where(x => x.Id == item.Id).FirstOrDefault();
                itemObj = item;
                itemObj.Rating = item.Rating;
              //  db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}