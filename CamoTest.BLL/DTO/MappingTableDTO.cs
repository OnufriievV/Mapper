using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamoTest.BLL.DTO
{
    public class MappingTableDTO
    {
        public string DisplayFileName { get; set; }

        public int RequestId { get; set; }

        public string HeadersErrors { get; set; }

        public List<string> Headers { get; set; }

        public List<MappingTableItemDTO> TableItemsDTO { get; set; }

        public MappingTableDTO()
        {
            TableItemsDTO = new List<MappingTableItemDTO>();
        }
    }

    public class MappingTableItemDTO
    {
        public string Name { get; set; }

        public string ValueExamples { get; set; }

        public string Errors { get; set; }
    }
}
