using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orthofi.OCR.DTOs;
using Orthofi.OCR.Mappers;
using System.Linq;

namespace Orthofi.OCR.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class GoogleMapperTests
    {
        public GoogleMapperTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetLinesReturnsProperlySplitLines()
        {
            List<string> expected = new List<string>();
            expected.Add("aetna");
            expected.Add("Managed Choice");
            expected.Add("NAME");

            DtoGoogleTextDetection dto = new DtoGoogleTextDetection();
            dto.Words = new List<TextNode>()
            {
                new TextNode
                {
                    locale="en",
                    boundingPoly = new BoundingPoly(),
                    description = "aetna NrDP\nBeschast\nStreet\nADP TOTALSOURCE, INC\nA more human resource?\nGRP: 326321-018-00020\nIssuer (80840) 9140860054\nManaged Choice\nOpen Access\nID W2434 86188\nNAME\n01 DAVID L APPEL\n02 KIRSTEN LOGAN\nPCP NO ELECTION REQUIRED\nPCP: NO ELECTION REQUIRED\nRX BIN# 6 1 0502\naetna NrDP\nBeschast\nStreet\nADP TOTALSOURCE, INC\nA more human resource?\nGRP: 326321-018-00020\nIssuer (80840) 9140860054\nManaged Choice\nOpen Access\nID W2434 86188\nNAME\n01 DAVID L APPEL\n02 KIRSTEN LOGAN\nPCP NO ELECTION REQUIRED\nPCP: NO ELECTION REQUIRED\nRX BIN# 6 1 0502\n"
                },
                    new 
                TextNode
                {
                    boundingPoly = new BoundingPoly(),
                    description = "aetna"
                }
            };

            GoogleTextDetectionMapper mapper = new GoogleTextDetectionMapper();
            var results = mapper.GetLines(dto);

            foreach (var item in expected)
            {
                var line = results.Where(l => l == item).First();
                Assert.IsNotNull(line);
            }

            Assert.IsNull(results.Where(l => l.Contains("\n")).FirstOrDefault());
        }
    }
}
