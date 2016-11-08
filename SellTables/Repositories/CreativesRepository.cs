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

        void IRepository<Creative>.Add(Creative item, ApplicationDbContext db)
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
            return db.Creatives.Include(c => c.Chapters).ToList();
        }

        public ICollection<Creative> GetRange(int start, int count, ApplicationDbContext dbc)
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

        void IRepository<Creative>.Update(Creative item, ApplicationDbContext db)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}