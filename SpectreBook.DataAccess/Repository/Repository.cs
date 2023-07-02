
using SpectreBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpectreBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();

        }

        void Remove(T entity)
        {
            throw new NotImplementedException();

        }

        void RemoveRange(T entity)
        {
            throw new NotImplementedException();

        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Add(T entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }
    }
}
