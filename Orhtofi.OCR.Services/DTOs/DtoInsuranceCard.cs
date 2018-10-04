using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR
{
    public class DtoInsuranceCard
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public string Issuer { get; set; }
        public string InsuranceName { get; set; }
        public string PlanName { get; set; }
        public string  Participant { get; set; }
        public string[] Dependents { get; set; }
    }
}
