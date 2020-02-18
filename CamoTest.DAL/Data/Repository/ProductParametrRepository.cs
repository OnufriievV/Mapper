using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Model;


namespace CamoTest.DAL.Data.Repository
{
    public interface IProductParameterRepository : IRepository<ProductParameter, long>
    {

    }

    public class ProductParameterRepository : RepositoryBase<ProductParameter, long>, IProductParameterRepository
    {
        public ProductParameterRepository(CamoTestEntities context) : base(context) { }
    }
}
