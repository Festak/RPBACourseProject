using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellTables.Interfaces
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> GetAll();

        ICollection<T> Find(Func<T, bool> predicate);

      T Get(int id);

        bool Remove(int id);

        void Add(T item);
        void Update(T item);
    }
}
