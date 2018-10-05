using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR.Processors
{
    public interface IImageTextProcessor
    {
        string GetResultsForImage(string imagePath);
        string GetResultsForImage(byte[] image);
    }
}
