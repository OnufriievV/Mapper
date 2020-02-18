using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Model;


namespace CamoTest.DAL.Data.Repository
{
    public interface IBrandRepository : IRepository<Brand, int>
    {

    }

    public class BrandRepository : RepositoryBase<Brand, int>, IBrandRepository
    {
        public BrandRepository(CamoTestEntities context) : base(context) { }
    }
}
