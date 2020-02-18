using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace CamoTest.DAL.Data.Infrastructure
{
    public interface IEntity<T>
    {
         T Id { get; set; }
    }

    public abstract class BaseEntity
    {

    }

    public abstract class Entity<T> : IEntity<T>
    {
        [System.ComponentModel.DataAnnotations.Key, Schema.DatabaseGenerated(Schema.DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
    }
}
