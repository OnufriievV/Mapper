using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Model;


namespace CamoTest.DAL.Data.Repository
{
    public interface IProductRepository : IRepository<Product, long>
    {

    }

    public class ProductRepository : RepositoryBase<Product, long>, IProductRepository
    {
        public ProductRepository(CamoTestEntities context) : base(context) { }
    }
}
