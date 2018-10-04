using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR.DTOs
{
    public  class DtoGoogleTextDetection
    {
        public IList<TextNode>  Words { get; set; }
    }

    public class Vertex
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingPoly
    {
        public IList<Vertex> vertices { get; set; }
    }

    public class TextNode
    {
        public string locale{ get; set;}

        public string description { get; set; }
        public BoundingPoly boundingPoly { get; set; }
    }

}
