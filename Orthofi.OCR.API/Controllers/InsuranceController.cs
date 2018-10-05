﻿using Orthofi.OCR.Mappers;
using Orthofi.OCR.Processors;
using Orthofi.OCR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Orthofi.OCR.API.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Mvc.RoutePrefix("insurance/card")]
    public class InsuranceController : Controller
    {

        Dictionary<int, string> pics = new Dictionary<int, string>();


        public InsuranceController()
        {
            pics.Add(1, "https://s3-us-west-2.amazonaws.com/appel-hack-too/aetna.JPG");
            pics.Add(2, "https://s3-us-west-2.amazonaws.com/appel-hack-too/delta+dental.JPG");
            pics.Add(3, "https://s3-us-west-2.amazonaws.com/appel-hack-too/delta-wash.JPG");
        }


        //// GET: Insurance
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id:int}")]
        public ActionResult Card(int id)
        {
            //TODO inject these
            IImageTextProcessor processor = new GoogleImageTextProcessor();
            IProcessorResultMapper mapper = new GoogleTextDetectionMapper();

            var results = processor.GetResultsForImage(pics[id]);
            var dto = mapper.MapResultsToDto(results);
            dto.ImageUrl = pics[id];

            var rtn = new JsonResult();

            rtn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rtn.Data = dto;

            return rtn;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Mvc.Route("")]
        public ActionResult PostCard([FromBody]string pic)
        {
            byte[] img = Convert.FromBase64String(pic);

            //TODO inject these
            IImageTextProcessor processor = new GoogleImageTextProcessor();
            IProcessorResultMapper mapper = new GoogleTextDetectionMapper();

            var results = processor.GetResultsForImage(img);
            var dto = mapper.MapResultsToDto(results);

            dto.ImageUrl = S3Service.UploadFileAsync(img, $"{dto.CarrierName}.jpg").Result;

            var rtn = new JsonResult();

            rtn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            rtn.Data = dto;

            return rtn;
        }

    }
}