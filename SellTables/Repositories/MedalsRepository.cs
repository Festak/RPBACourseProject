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
    public class MedalsRepository : IRepository<Medal>
    {
        private readonly ApplicationDbContext db;

        public MedalsRepository()
        {
            db = new ApplicationDbContext();
        }

        void IRepository<Medal>.Add(Medal item)
        {
            db.Medals.Add(item);
        }

        ICollection<Medal> IRepository<Medal>.Find(Func<Medal, bool> predicate)
        {
            return db.Medals.Where(predicate).ToList();
        }

        async Task<Medal> IRepository<Medal>.Get(int id)
        {
            return await db.Medals.FindAsync(id);
        }

        ICollection<Medal> IRepository<Medal>.GetAll()
        {
            return db.Medals.ToList();
        }

        async Task<bool> IRepository<Medal>.Remove(int id)
        {
            var Medal = await db.Medals.FindAsync(id);

            if (Medal != null)
            {
                db.Medals.Remove(Medal);
                return true;
            }

            return false;
        }

        void IRepository<Medal>.Update(Medal item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}