using System;
using System.Collections.Generic;
using System.Linq;

namespace Orthofi.OCR.Mappers
{
    public class GoogleTextDetectionMapper : IProcessorResultMapper
    {
        public DtoInsuranceCard MapResultsToDto(string json)
        {
            throw new NotImplementedException();
        }

        //TODO fix acces modifiers and figure out to test
        public DTOs.DtoGoogleTextDetection Deserialize(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject< DTOs.DtoGoogleTextDetection>(json);
        }

        public List<string> GetLines(DTOs.DtoGoogleTextDetection dto)
        {
            var mainLine = dto.Words?.Where(w => !string.IsNullOrEmpty(w.locale)).Single();

            if (mainLine != null)
                return mainLine.description.Split("\n".ToCharArray()).ToList();
            else
                return new List<string>();
        }

        

    }
}
