using CamoTest.DAL.Data.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CamoTest.DAL.Model
{
    public class Brand : Entity<int>
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Brand()
        {
            this.Products = new List<Product>();
        }
    }
}
