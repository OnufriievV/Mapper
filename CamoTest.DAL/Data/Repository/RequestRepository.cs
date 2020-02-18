using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Model;


namespace CamoTest.DAL.Data.Repository
{
    public interface IRequestRepository : IRepository<Request, int>
    {

    }

    public class RequestRepository : RepositoryBase<Request, int>, IRequestRepository
    {
        public RequestRepository(CamoTestEntities context) : base(context) { }
    }
}
