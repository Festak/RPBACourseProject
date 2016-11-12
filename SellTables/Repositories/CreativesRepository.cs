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
        private readonly ApplicationDbContext dataBaseContext;

        public CreativesRepository(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        void IRepository<Creative>.Add(Creative item)
        {
           
                dataBaseContext.Creatives.Add(item);
                dataBaseContext.SaveChanges();
            
        }

        ICollection<Creative> IRepository<Creative>.Find(Func<Creative, bool> predicate)
        {
            return dataBaseContext.Creatives.Where(predicate).ToList();
        }

        Creative IRepository<Creative>.Get(int id)
        {
            return dataBaseContext.Creatives.Find(id);
        }

        ICollection<Creative> IRepository<Creative>.GetAll()
        {
            return dataBaseContext.Creatives.Include(c => c.Chapters).Include(u => u.User).ToList();
        }
        // TODO: replace ifs
        public ICollection<Creative> GetRange(int start, int count, int sortType)
        {
           
                IEnumerable<Creative> result = null;
                if (sortType == 1) {
                    result = GetDateSortedCreatives();
                }
                if (sortType == 2) {
                    result = GetEditDateSortedCreatives();
                }
                if (sortType == 3) {
                    result = GetNameSortedCreatives();
                }
                else {
                    result = GetDateSortedCreatives();
                }
                
                
                return result.Skip(start - 1).Take(count).ToList();
            }
        

        private IEnumerable<Creative> GetDateSortedCreatives() {
            return dataBaseContext.Creatives.Include(c => c.Chapters).Include(u => u.User).Include(m=>m.User.Medals).OrderByDescending(c => c.CreationDate);
        }

        private IEnumerable<Creative> GetEditDateSortedCreatives()
        {
            return dataBaseContext.Creatives.Include(c => c.Chapters).Include(u => u.User).Include(m => m.User.Medals).OrderBy(c => c.Id);
        }

        private IEnumerable<Creative> GetNameSortedCreatives()
        {
            return dataBaseContext.Creatives.Include(c => c.Chapters).Include(u => u.User).Include(m => m.User.Medals).OrderBy(c => c.Name);
        }
     

        public ICollection<Creative> GetPopular()
        {
            int count = dataBaseContext.Creatives.Where(p => p.Rating >= 4).Count();
            if (count > 10)
            {
                return dataBaseContext.Creatives.Include(c => c.Chapters).Include(u => u.User).Where(p => p.Rating >= 4).Take(10).OrderBy(p => p.Rating).ToList();
            }
            else {
                return dataBaseContext.Creatives.Include(c => c.Chapters).Include(u => u.User).Where(p => p.Rating >= 4).Take(count).OrderBy(p => p.Rating).ToList();
            }
        }

        bool IRepository<Creative>.Remove(int id)
        {
            var Creative = dataBaseContext.Creatives.Find(id);

            if (Creative != null)
            {
                dataBaseContext.Creatives.Remove(Creative);
                return true;
            }

            return false;
        }

        void IRepository<Creative>.Update(Creative item)
        {
            if (item != null)
            {
                var _Item = dataBaseContext.Entry(item);
                Creative itemObj = dataBaseContext.Creatives.Where(x => x.Id == item.Id).FirstOrDefault();
                itemObj = item;
                itemObj.Rating = item.Rating;
              //  dataBaseContext.Entry(item).State = EntityState.Modified;
                dataBaseContext.SaveChanges();
            }
        }
    }
}