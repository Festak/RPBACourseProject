﻿using SellTables.Interfaces;
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
        // TODO: replace ifs
        public ICollection<Creative> GetRange(int start, int count, int sortType, ApplicationDbContext dbc)
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

        void IRepository<Creative>.Update(Creative item, ApplicationDbContext db)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}