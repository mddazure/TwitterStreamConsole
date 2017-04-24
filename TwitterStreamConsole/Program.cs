using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Stream = Tweetinvi.Stream;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;


namespace TwitterStreamConsole
{
        
    class Program
    {
        private const string EndpointUrl = "https://twitterdocdb.documents.azure.com:443/";
        private const string PrimaryKey = "2IbiTHlVga2EJS7tog53rfc4pebRdUyNeEXVK9F8vEnsdzezct94kQYVKOXjMZ2tj0Z9gCzBVzDIX0pxCcbj4Q==";
        private const string DataBase = "TwitterDB";
        private const string Collection = "TwitterCol";
        
        private DocumentClient client;

        private const string accessToken = "40526326-3DD6sWGTspKBYwV3Xbl7jTjwlRGcaqZmYtkYSpUPs";
        private const string accessTokenSecret = "zEXoShxAVETPwafWWUbD1Bytcq9utBYTOrylLfcuclhPr";
        private const string consumerKey = "uJSMzGeA4Zc4E2orOfk0nkFs6";
        private const string consumerSecret = "cxOnWo2zikN6WEa2Mxo7DtDy4pDsZJoiQDfHYTCx2xaRPQFqEE";
        private const string userId = "md1746";
                
        static void Main()
        {
            Program p = new Program();
            p.StartDocDB().Wait();
            
            



           Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            var sampleStream = Stream.CreateSampleStream();
            sampleStream.TweetReceived += (sender, args) => 
            {

               //Console.WriteLine(args.ToJson());
                try
                {
                    
                    p.CreateTweetDocumentIfNotExists(DataBase, Collection, args.Tweet).Wait();
                }
                catch (DocumentClientException de)
                {
                    Exception baseException = de.GetBaseException();
                    Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                }
                catch (Exception e)
                {
                    Exception baseException = e.GetBaseException();
                    Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                }
               /* finally
                {
                    Console.WriteLine(args.Tweet.ToJson());
                }*/


            };
            sampleStream.StartStream(); 
                

        }

        private async Task StartDocDB()
        {
            this.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = DataBase });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DataBase), new DocumentCollection { Id = Collection});

        }

        private async Task CreateTweetDocumentIfNotExists(string databaseName, string collectionName, Tweetinvi.Models.ITweet tweet)
        {
            
            
            await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DataBase, Collection), tweet,null , true);
        }
            

        
       

    }
}
