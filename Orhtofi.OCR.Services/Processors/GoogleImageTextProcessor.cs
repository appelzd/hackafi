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
        public string GetResultsForImage(string imagePath, bool isFile)
        {
            var client = GetClient();
            try
            {
                // Load the image file into memory
                Image image = null;

                if(!isFile)
                    image =Image.FetchFromUri(imagePath);
                else
                    image = Image.FromFile(imagePath);

                var response = client.DetectDocumentTextAsync(image).Result;

                return response.Text;

            }
            catch (Exception e)
            {
                var r = e.Message;
                throw;
            }

            throw new NotImplementedException();
        }

        public string GetResultsForImage(byte[] image)
        {
            var client = GetClient();
            try
            {
                // Load the image file into memory
                var img = Image.FromBytes(image);

                var response = client.DetectDocumentTextAsync(img).Result;

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
