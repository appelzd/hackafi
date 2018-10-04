using Google.Apis.Auth.OAuth2;
using Google.Cloud.Language.V1;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orthofi.OCR.Processors
{
    public class GoogleImageTextProcessor : IImageTextProcessor
    {
        
        ImageAnnotatorClient GetClient()
        {
            var credential = GoogleCredential.FromFile(@"C:\Users\David Appel\Documents\creds\TextProcessor.json").CreateScoped(LanguageServiceClient.DefaultScopes);
            var channel = new Grpc.Core.Channel(ImageAnnotatorClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());

            // Instantiates a client
            return ImageAnnotatorClient.Create(channel);

        }

        //TODO we should use a factory, which we inject to determin the client, etc.
        //that way, we have testability and plugability
        public string GetResultsForImage(string imagePath)
        {
            var client = GetClient();
            try
            {
                // Load the image file into memory
                var image = Image.FromFile(@"C:\Users\David Appel\Pictures\insurance.jpg");

                var response = client.DetectDocumentTextAsync(image).Result;

                //foreach (var page in response.Pages)
                //{
                //    foreach (var block in page.Blocks)
                //    {
                //        foreach (var paragraph in block.Paragraphs)
                //        {
                //            System.Diagnostics.Debug.WriteLine(string.Join("\n", paragraph.Words));
                //        }
                //    }
                //}

                return response.Text;

            }
            catch (Exception e)
            {
                var r = e.Message;
                throw;
            }

            throw new NotImplementedException();
        }
    }
}
