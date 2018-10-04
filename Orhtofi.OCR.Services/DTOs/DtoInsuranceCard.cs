using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR
{
    public class DtoInsuranceCard
    {
        public string MemberId { get; set; }

        public string Group { get; set; }
        public string Issuer { get; set; }
        public string CarrierName { get; set; }
        public string CarrierType { get; set; }
        public string PlanName { get; set; }
        public string  Participant { get; set; }
        public string[] Dependents { get; set; }
        public string  ImageUrl { get; set; }
    }
}
