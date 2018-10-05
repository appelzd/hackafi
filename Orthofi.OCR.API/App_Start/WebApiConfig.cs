using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Orthofi.OCR.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Formatters.Add(new TextMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            var corsAtt = new EnableCorsAttribute(
                origins: "http://localhost:4200",
                headers: "*",
                methods: "*");

            config.EnableCors(corsAtt);
            
        }
    }
}
