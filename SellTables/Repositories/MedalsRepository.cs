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
        private readonly ApplicationDbContext dataBaseContext;

        public MedalsRepository(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        void IRepository<Medal>.Add(Medal item)
        {
            dataBaseContext.Medals.Add(item);
        }

        ICollection<Medal> IRepository<Medal>.Find(Func<Medal, bool> predicate)
        {
            return dataBaseContext.Medals.Where(predicate).ToList();
        }

        Medal IRepository<Medal>.Get(int id)
        {
            return dataBaseContext.Medals.Find(id);
        }

        ICollection<Medal> IRepository<Medal>.GetAll()
        {
            return dataBaseContext.Medals.ToList();
        }

        bool IRepository<Medal>.Remove(int id)
        {
            var Medal = dataBaseContext.Medals.Find(id);

            if (Medal != null)
            {
                dataBaseContext.Medals.Remove(Medal);
                return true;
            }

            return false;
        }

        void IRepository<Medal>.Update(Medal item)
        {
            dataBaseContext.Entry(item).State = EntityState.Modified;
        }
    }
}