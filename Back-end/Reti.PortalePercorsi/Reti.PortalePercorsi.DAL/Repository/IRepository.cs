using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Reti.PortalePercorsi.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(int Id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
