using Orthofi.OCR.Mappers;
using Orthofi.OCR.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Orthofi.OCR.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InsuranceController : Controller
    {
        // GET: Insurance
        public ActionResult Index()
        {
            string url = @"C:\Users\David Appel\Pictures\insurance.jpg";
            //TODO inject these
            IImageTextProcessor processor = new GoogleImageTextProcessor();
            IProcessorResultMapper mapper = new GoogleTextDetectionMapper();

            var results = processor.GetResultsForImage(url);
            var dto = mapper.MapResultsToDto(results);
            dto.ImageUrl = url;

            var rtn = new JsonResult();
            
            rtn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rtn.Data = dto;

            return rtn;
        }
    }
}