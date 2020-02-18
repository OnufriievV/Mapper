using CamoTest.BLL.Service;
using CamoTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CamoTest.Controllers
{
    public class ErrorsController : ApiController
    {
        public IFileService FileService { get; set; }

        public IHttpActionResult Get([FromUri] int[] selectorsValues, [FromUri] int? requestId)
        {
            if (selectorsValues == null || requestId == null || requestId == 0)
                return NotFound();

            var errorsArr = FileService.GetErrorList(requestId.Value, selectorsValues, HttpContext.Current.Server.MapPath("~/App_Data/"), CamoTestConfiguration.TemporaryFileExtension);

            if (errorsArr == null)
                return NotFound();

            if (errorsArr.Any(s => !String.IsNullOrWhiteSpace(s)))
                return Ok(errorsArr);

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}