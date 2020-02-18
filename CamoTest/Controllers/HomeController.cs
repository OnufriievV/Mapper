using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CamoTest.BLL.Service;
using CamoTest.Services;
using CamoTest.ViewModels;
using CamoTest.Properties;

namespace CamoTest.Controllers
{
    public class HomeController : Controller
    {
        public IFileService FileService { get; set; }

        public IHangFireService HangFireService { get; set; }

        public ActionResult Index(string error)
        {
            if (!String.IsNullOrWhiteSpace(error))
                ModelState.AddModelError(String.Empty, error);
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {

            if (file == null || file.ContentLength < 1)
                return RedirectToAction("Index", "Home", new { error = Resources.FileIsNotSelectedOrHasZeroLength });

            string path = Server.MapPath("~/App_Data/");
            string fileExt = CamoTestConfiguration.TemporaryFileExtension;
            int temporaryFileStorageTime = CamoTestConfiguration.TemporaryFileStorageTime;

            int requestId = FileService.SaveFile(file.InputStream, path, fileExt, file.FileName, temporaryFileStorageTime);
            
            if(requestId == 0)
                return RedirectToAction("Index", "Home");

            
            var mappingTableDTO =  FileService.GetMappingTableDTO(requestId, path, fileExt);

            MappingTableVM mappingTableVM;

            if (mappingTableDTO != null)
            {
                mappingTableVM = mappingTableDTO;
                //if (mappingTableVM.TableItemsVM.Any(i => i.SelectListItems.Any(s => s.Value != "0")))
                //{
                //    var selectorsValues = mappingTableVM.TableItemsVM.Select(i => Int32.Parse(i.SelectListItems.Last(s => s.Selected == true).Value)).ToArray();
                //    var rowErrors = FileService.GetErrorList(requestId, selectorsValues, Server.MapPath("~/App_Data/"), CamoTestConfiguration.TemporaryFileExtension);
                //    for (int i = 0; i < rowErrors.Length; i++)
                //        mappingTableVM.TableItemsVM[i].Errors = rowErrors[i];
                //}

                return View("MappingTable", mappingTableVM);
            }
                
      

            return View();
        }

        //[HttpPost]
        //public ActionResult Upload(int [] selectorsValues, string fileName)
        //{


        //    return Json(new { }, JsonRequestBehavior.AllowGet);
        //}

    }
}