using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CamoTest.DAL.Data.Infrastructure
{
    public interface IRepository<T, Tid> where T : IEntity<Tid>
    {
        Tid Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //void Delete(Expression<Func<T, bool>> where);
        T GetById(Tid id);
        //T GetById(string id);
        //T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAllForReadOnly();
        //IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        //IQueryable<T> GetAny();
    }

    public class RepositoryBase<Tentity, Tid> : IRepository<Tentity, Tid> where Tentity : class, IEntity<Tid>
    {
        private readonly CamoTestEntities _context;
        private readonly IDbSet<Tentity> _dbset;

        public RepositoryBase (CamoTestEntities context)
        {
            _context = context;
            _dbset = _context.Set<Tentity>();
        }

        public Tid Add(Tentity entity)
        {
            var item =_dbset.Add(entity);
            return item.Id;
        }

        public void Delete(Tentity entity)
        {
            _dbset.Remove(entity);
        }

        public void Update(Tentity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Tentity GetById(Tid id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<Tentity> GetAll()
        {
            return _dbset.ToList();
        }
    }
}
