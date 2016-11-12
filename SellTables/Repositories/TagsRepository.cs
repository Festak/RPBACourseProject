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
    public class TagsRepository : IRepository<Tag>
    {

        private readonly ApplicationDbContext dataBaseContext;

        public TagsRepository(ApplicationDbContext dataBaseContext) {
            this.dataBaseContext = dataBaseContext;
        }

        void IRepository<Tag>.Add(Tag item)
        {
          
                dataBaseContext.Tags.Add(item);
                dataBaseContext.SaveChanges();
            
        }

        ICollection<Tag> IRepository<Tag>.Find(Func<Tag, bool> predicate)
        {
            return dataBaseContext.Tags.Where(predicate).ToList();
        }

        Tag IRepository<Tag>.Get(int id)
        {
            return dataBaseContext.Tags.Find(id);
        }

        ICollection<Tag> IRepository<Tag>.GetAll()
        {
            return dataBaseContext.Tags.ToList();
        }

        bool IRepository<Tag>.Remove(int id)
        {
            var Tag = dataBaseContext.Tags.Find(id);

            if (Tag != null)
            {
                dataBaseContext.Tags.Remove(Tag);
                return true;
            }

            return false;
        }

        void IRepository<Tag>.Update(Tag item)
        {
            dataBaseContext.Entry(item).State = EntityState.Modified;
        }
    }
}