using Microsoft.EntityFrameworkCore;
using Reti.PortalePercorsi.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reti.PortalePercorsi.DAL.Repository
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private PortalePercorsiContext Context;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = Context.Set<TEntity>();
                }
                return _dbSet;
            }
        }

        public Repository(PortalePercorsiContext context)
        {
            this.Context = context;
            
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public TEntity GetByID(int Id)
        {
            return DbSet.Find(Id);
        }
    }
}
