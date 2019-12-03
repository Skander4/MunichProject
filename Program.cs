using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Models;

namespace MunichService
{
    public class Program
    {

    


        public static void Main(string[] args)
        {

            
            var client = new MongoClient("mongodb+srv://LafiSkander:skanderxmp4@clustermunich-zdfnm.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("MongoDB");
            IMongoCollection<tweet> _tweet;
            _tweet = database.GetCollection<tweet>("TweetTable");

            

            Auth.SetUserCredentials("7qcZNJlyfJqghqSShOTHoH4uq", "cAtjKXyC5AGA1u2Q5XefLhnhUbkNVwQIi949ngMKA7Pqi8cSQr", "1201837079535267840-ksEkSOOhMJbAjHu25FrF6BPEB8yDs6", "QV8QgtFEaN5exz4GJ3FHXw7QAkyT6ytXdv44s4SmYFiwS");

            var user = User.GetAuthenticatedUser();

            var searchParameter = Search.CreateTweetSearchParameter("#startup");

          
            searchParameter.SearchType = SearchResultType.Recent;
            searchParameter.MaximumNumberOfResults = 30;
            searchParameter.Since = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);

            var tweets = Search.SearchTweets(searchParameter);
            //  tweets.ForEach(t => Console.WriteLine(t.Text));


            tweets.ForEach(t => _tweet.InsertOne(new tweet
            {
                Tweet = t.Text
            }
            ));



            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });

  
    }
}
