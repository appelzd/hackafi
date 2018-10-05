using Newtonsoft.Json;
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
            var lines = json.Split("\n".ToCharArray()).ToList();
            return GetCardFromResults(lines);
        }

        public DtoInsuranceCard GetCardFromResults(List<string> lines)
        {
            DtoInsuranceCard card = new DtoInsuranceCard();
            card.CarrierName = GetValueFromLines(lines, GetRegexesForCarrier());
            card.Group = GetValueFromLines(lines, GetRegexesForGroup());
            card.MemberId = GetValueFromLines(lines, GetRegexesForMemberId());
            card.CarrierType = GetValueFromLines(lines, GetRegexesForCarrierTypeLocation());
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
                "ID",
                "id",
                "Identification",
                "Member Number"
            };
        }

        #endregion Member ID


        #region carriers

        //TODO we should pull this from DB
        IList<string> GetRegexesForCarrier()
        {
            return new List<string>
            {
                "aetna",
                "DELTA"
            };
        }

        #endregion carriers

        #region 
        IList<string> GetRegexesForCarrierTypeLocation()
        {
            return new List<string>
            {
                "PPO",
                "POS",
                "Dental",
                "HMO"
            };
        }

        #endregion

        #region groups

        IList<string> GetRegexesForGroup()
        {
            return new List<string>
            {
                "GRP",
                "Group",
                "grp"
            };
        } 
        #endregion

    }
}
