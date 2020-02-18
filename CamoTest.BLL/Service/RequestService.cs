using CamoTest.BLL.Infrastructure;
using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Model;

namespace CamoTest.BLL.Service
{
    public interface IRequestService : IBaseService<Request, int>
    {
        int Create(Request entity, string path, string extension);
    }


    public class RequestService : BaseService<Request, int>, IRequestService
    {
        public RequestService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public int Create(Request entity, string path, string extension)
        {
            _repository.Add(entity);
            Save();

            return entity.Id;
        }
    }
}
