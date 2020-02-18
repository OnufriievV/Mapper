using CamoTest.DAL.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamoTest.DAL.Model
{
    public class Request : Entity<int>
    {
        [Required, MaxLength(255)]
        public string UploadedFileName { get; set; }

        [Required]
        public Guid RealFileName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
