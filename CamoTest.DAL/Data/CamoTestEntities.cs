namespace CamoTest.DAL.Data
{
    using CamoTest.DAL.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CamoTestEntities : DbContext
    {
        // Your context has been configured to use a 'CamoTestEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CamoTest.DAL.Data.CamoTestEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CamoTestEntities' 
        // connection string in the application configuration file.
        static CamoTestEntities()
        {
            //Database.SetInitializer(new MyContextInitializer());
        }

        public CamoTestEntities()
            : base("name=CamoTestEntities")
        {
        }

        


        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<ProductParameter> ProductParameters { get; set; }
        public virtual DbSet<Request> Requests { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().Property(p => p.Price).Has
        //}
    }

    class MyContextInitializer : DropCreateDatabaseAlways<CamoTestEntities>
    {
        protected override void Seed(CamoTestEntities db)
        {

        }
    }
}