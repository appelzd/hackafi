﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR.Mappers
{
    public interface IProcessorResultMapper
    {
        DtoInsuranceCard MapResultsToDto(string json);
    }
}
