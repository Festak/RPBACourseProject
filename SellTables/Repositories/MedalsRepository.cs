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

        public MedalsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        void IRepository<Medal>.Add(Medal item)
        {
            db.Medals.Add(item);
        }

        ICollection<Medal> IRepository<Medal>.Find(Func<Medal, bool> predicate)
        {
            return db.Medals.Where(predicate).ToList();
        }

        Medal IRepository<Medal>.Get(int id)
        {
            return db.Medals.Find(id);
        }

        ICollection<Medal> IRepository<Medal>.GetAll()
        {
            return db.Medals.ToList();
        }

        bool IRepository<Medal>.Remove(int id)
        {
            var Medal = db.Medals.Find(id);

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