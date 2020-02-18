using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamoTest.DAL.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        CamoTestEntities Get();
    }

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private CamoTestEntities dataContext;
        public CamoTestEntities Get()
        {
            return dataContext ?? (dataContext = new CamoTestEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
