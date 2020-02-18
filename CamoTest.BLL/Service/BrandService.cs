using CamoTest.BLL.Infrastructure;
using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Model;


namespace CamoTest.BLL.Service
{
    public interface IBrandService : IBaseService<Brand, int>
    {

    }


    public class BrandService : BaseService<Brand, int>, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork) : base (unitOfWork)
        {

        }
    }
}
