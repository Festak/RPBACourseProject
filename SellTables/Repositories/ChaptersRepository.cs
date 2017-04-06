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
        private readonly ApplicationDbContext dataBaseContext;

        public ChaptersRepository(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;  
        }

        void IRepository<Chapter>.Add(Chapter item)
        {
            dataBaseContext.Chapters.Add(item);
        }

        ICollection<Chapter> IRepository<Chapter>.Find(Func<Chapter, bool> predicate)
        {
            return dataBaseContext.Chapters.Where(predicate).ToList();
        }

        Chapter IRepository<Chapter>.Get(int id)
        {
            return dataBaseContext.Chapters.Find(id);
        }

        ICollection<Chapter> IRepository<Chapter>.GetAll()
        {
            return dataBaseContext.Chapters.ToList();
        }

        bool IRepository<Chapter>.Remove(int id)
        {
            var Chapter = dataBaseContext.Chapters.Find(id);

            if (Chapter != null)
            {
                dataBaseContext.Chapters.Remove(Chapter);
                return true;
            }

            return false;
        }

        void IRepository<Chapter>.Update(Chapter item)
        {
            if (item != null)
            {
                var _Item = dataBaseContext.Entry(item);
                Chapter itemObj = dataBaseContext.Chapters.Where(x => x.Id == item.Id).FirstOrDefault();
                itemObj = item;
                dataBaseContext.SaveChanges();
            }
        }
    }
}