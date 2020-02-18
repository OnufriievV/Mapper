using Autofac;
using CamoTest.BLL.Service;
using CamoTest.DAL.Data.Infrastructure;
using CamoTest.DAL.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace CamoTest.BLL.Infrastructure
{
    public class AutofacRegister
    {
        public static void AutofacRegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

           
            builder.RegisterType<RequestRepository>().As<IRequestRepository>().InstancePerLifetimeScope();



            builder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope().PropertiesAutowired();

            builder.RegisterType<RequestService>().As<IRequestService>().InstancePerLifetimeScope().PropertiesAutowired();
           

            builder.RegisterType<HangFireService>().As<IHangFireService>().InstancePerLifetimeScope().PropertiesAutowired();



        }
    }
}
