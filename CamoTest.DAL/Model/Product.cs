using CamoTest.DAL.Data.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CamoTest.DAL.Model
{
    public class Product : Entity<long>
    {
        [Required, MaxLength(50)]
        public string SKU { get; set; }

        [Required]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        
        [Required]
        public float Price { get; set; }

        public float? Weight { get; set; }

        public virtual ICollection<ProductParameter> ProductParametrs { get; set; }
    }
}
