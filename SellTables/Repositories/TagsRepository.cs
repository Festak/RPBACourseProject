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

        private readonly ApplicationDbContext db;

        public TagsRepository() {
            db = new ApplicationDbContext();
        }

        void IRepository<Tag>.Add(Tag item, ApplicationDbContext db)
        {
          
                db.Tags.Add(item);
                db.SaveChanges();
            
        }

        ICollection<Tag> IRepository<Tag>.Find(Func<Tag, bool> predicate)
        {
            return db.Tags.Where(predicate).ToList();
        }

        Tag IRepository<Tag>.Get(int id)
        {
            return db.Tags.Find(id);
        }

        ICollection<Tag> IRepository<Tag>.GetAll()
        {
            return db.Tags.ToList();
        }

        bool IRepository<Tag>.Remove(int id)
        {
            var Tag = db.Tags.Find(id);

            if (Tag != null)
            {
                db.Tags.Remove(Tag);
                return true;
            }

            return false;
        }

        void IRepository<Tag>.Update(Tag item, ApplicationDbContext db)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}