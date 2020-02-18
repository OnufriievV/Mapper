using CamoTest.DAL.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamoTest.DAL.Data.Infrastructure
{
    public interface IUnitOfWork
    {

        IRepository<Tentity, Tid> GetRepository<Tentity, Tid>() where Tentity : class, IEntity<Tid>;
        //IBrandRepository BrandRepository { get; set; }

        //IProductRepository ProductRepository { get; set; }

        //IProductParameterRepository ProductParameterRepository { get; set; }

        void SaveChanges();
    }

    //interface IUnitOfWork
    //{
    //    IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    //    int SaveChanges();
    //}

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CamoTestEntities _context;

        public IRepository<Tentity, Tid> GetRepository<Tentity, Tid>() where Tentity : class, IEntity<Tid>
        {
            return new RepositoryBase<Tentity, Tid>(_context);
        }

        //public IBrandRepository BrandRepository { get; set; }

            //public IProductRepository ProductRepository { get; set; }

            // public IProductParameterRepository ProductParameterRepository { get; set; }

        public UnitOfWork (IDatabaseFactory databaseFactory)
        {
            _context = databaseFactory.Get();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
