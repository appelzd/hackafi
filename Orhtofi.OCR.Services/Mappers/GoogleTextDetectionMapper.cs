using Orthofi.OCR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Orthofi.OCR.Mappers
{
    public class GoogleTextDetectionMapper : IProcessorResultMapper
    {
        public DtoInsuranceCard MapResultsToDto(string json)
        {
            throw new NotImplementedException();
        }

        public DtoInsuranceCard GetCardFromResults(DtoGoogleTextDetection dto)
        {
            DtoInsuranceCard card = new DtoInsuranceCard();
            var lines = GetLines(dto);
            card.CarrierName = GetValueFromLines(lines, GetRegexesForCarrier());
            card.Group = GetValueFromLines(lines, GetRegexesForGroup());
            card.MemberId = GetValueFromLines(lines, GetRegexesForMemberId());
            return card;
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

        public string GetValueFromLines(List<string> lines, IList<string> regexStrings)
        {
            foreach (var matchString in regexStrings)
            {
                var regex = new Regex(matchString);
                var line = lines.Where(l => regex.IsMatch(l)).FirstOrDefault();

                if (line != null)
                    return line;
            }
            return null;
        }

        #region Member ID

        //TODO we should pull this from DB
        IList<string> GetRegexesForMemberId()
        {
            return new List<string>
            {
                @"\ID",
                @"\id"
            };
        }

        #endregion Member ID


        #region carriers

        //TODO we should pull this from DB
        IList<string> GetRegexesForCarrier()
        {
            return new List<string>
            {
                @"\aetna"
            };
        }

        #endregion carriers

        #region groups
       

        IList<string> GetRegexesForGroup()
        {
            return new List<string>
            {
                @"\GRP",
                @"\Group",
                @"\grp"
            };
        } 
        #endregion

    }
}
