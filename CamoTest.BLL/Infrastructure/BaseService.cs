using CamoTest.DAL.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamoTest.BLL.Infrastructure
{
    public interface IBaseService<T, Tid> where T : IEntity<Tid>
    {
        T GetByID(Tid id);

        IEnumerable<T> GetAll();

        //IEnumerable<T> GetAllForReadOnly();
    }

    public abstract class BaseService<Tentity, Tid> : IBaseService<Tentity, Tid> where Tentity : class, IEntity<Tid>
    {
        protected readonly IRepository<Tentity, Tid> _repository;

        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Tentity, Tid>();
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Tentity> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Tentity> GetAllForReadOnly()
        {
            throw new NotImplementedException();
        }

        public Tentity GetByID(Tid id)
        {
            return _repository.GetById(id) ;
        }

        public void Save()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
