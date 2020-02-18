using CamoTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CamoTest.Properties;

namespace CamoTest.ViewModels
{
    public class MappingTableVM
    {
        public string DisplayFileName { get; set; }

        public int RequestId { get; set; }

        public string HeaderErrors { get; set; }

        public List<string> Headers { get; set; }

        public List<MappingTableItemVM> TableItemsVM { get; set; }

        //public MappingTableVM(string displayFileName, int requestId, IEnumerable<MappingTableDTO> tableItems)
        //{
        //    DisplayFileName = displayFileName;
        //    RequestId = requestId;
        //    Headers = new List<string>() { "Колонка", "Параметр", "Пример значения", "Ошибки"};
        //    TableItems = new List<MappingTableItemVM>();
        //    foreach (var item in tableItems)
        //        TableItems.Add(item);

        //}

        public MappingTableVM()
        {
            TableItemsVM = new List<MappingTableItemVM>();
        }

        public static implicit operator MappingTableVM(MappingTableDTO item)
        {
            var newItem = new MappingTableVM
            {
                DisplayFileName = item.DisplayFileName,
                HeaderErrors = item.HeadersErrors,
                Headers = new List<string>() { Resources.Column, Resources.Parameter, Resources.ValueExamples, Resources.Errors },
                RequestId = item.RequestId
            };

            foreach (var i in item.TableItemsDTO)
                newItem.TableItemsVM.Add(i);
            
            return newItem;
        }

    }

    public class MappingTableItemVM
    {
        public string Name { get; set; }

        public IEnumerable<SelectListItem> SelectListItems { get; set; }

        public string ValueExamples { get; set; }

        public string Errors { get; set; }

        public static implicit operator MappingTableItemVM(MappingTableItemDTO item)
        {
            return new MappingTableItemVM()
            {
                Name = item.Name,
                SelectListItems = new List<SelectListItem>()
                {
                    new SelectListItem(){Value = "0", Text = "NoMapped", Selected = true},
                    new SelectListItem(){Value = "1", Text = "SKU", Selected = item.Name == "SKU" ? true : false},
                    new SelectListItem(){Value = "2", Text = "Brand", Selected = item.Name == "Brand" ? true : false},
                    new SelectListItem(){Value = "3", Text = "Price", Selected = item.Name == "Price" ? true : false},
                    new SelectListItem(){Value = "4", Text = "Weight", Selected = item.Name == "Weight" ? true : false},
                    new SelectListItem(){Value = "5", Text = "Feature", Selected = false},
                    new SelectListItem(){Value = "6", Text = "Product parameter​", Selected = false},
                    new SelectListItem(){Value = "7", Text = "Ignore​", Selected = false},

                    //new SelectListItem(){Value = "0", Text = "NoMapped", Selected = false},
                    //new SelectListItem(){Value = "1", Text = "SKU", Selected =  false},
                    //new SelectListItem(){Value = "2", Text = "Brand", Selected =  false},
                    //new SelectListItem(){Value = "3", Text = "Price", Selected =  false},
                    //new SelectListItem(){Value = "4", Text = "Weight", Selected =  false},
                    //new SelectListItem(){Value = "5", Text = "Feature", Selected = false},
                    //new SelectListItem(){Value = "6", Text = "Product parameter​", Selected = false},
                    //new SelectListItem(){Value = "7", Text = "Ignore​", Selected = false},

                },
                Errors = item.Errors ?? "",
                ValueExamples = item.ValueExamples
            };
        }
    }
}