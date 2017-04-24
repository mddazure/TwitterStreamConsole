using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tweetinvi;
using Stream = Tweetinvi.Stream;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;


namespace SendTweet
{
    class Program
    {
        private const string accessToken = "40526326-MeENaSPrRTy6m4KG4d3m624OFsttSADII27M7OtIk";
        private const string accessTokenSecret = "kJs5N8nbpGaV4B7Juks3UP2V2K6sfZ1lTh1h0gsIShDjJ";
        private const string consumerKey = "76m6AY9wl1QH6t2kA5lXPvAAH";
        private const string consumerSecret = "NB4ZgliCgKkXa8vM4feTeZh554WsSs40B87d9obipdb9yd7Vco";
        private const string userId = "md1746";

        private const string messagestart = "test message number ";

        static void Main()
        {
            
            Random rnd = new Random();
            int seqstart = rnd.Next(0, 1000);
            int counter = seqstart;
            string message;

            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            while(true)
            {
                message = messagestart + counter.ToString();
                Tweet.PublishTweet(message);
                counter++;
                Thread.Sleep(5000);
            }




        }
    }
}
