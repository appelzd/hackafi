using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR.Processors
{
    public interface IImageTextProcessor
    {
        dynamic GetResultsForImage(string imagePath);

    }
}
