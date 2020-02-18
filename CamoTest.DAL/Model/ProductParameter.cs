using CamoTest.DAL.Data.Infrastructure;


namespace CamoTest.DAL.Model
{
    public class ProductParameter : Entity<long>
    {
        public string Parametr { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
