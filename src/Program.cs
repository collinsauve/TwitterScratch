using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TwitterScratch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.MainAsync(args).Wait();
            Console.ReadKey();
        }

        public async Task MainAsync(string[] args)
        {
            var credentialses = GetCredentialses();
            foreach (var credentials in credentialses)
            {
                await DoUser(credentials);    
            }
        }

        private static IEnumerable<TwitterCredentials> GetCredentialses()
        {
            using (var ctx = new CredentialsContext())
            {
                //TODO: This isn't async...
                return ctx.TwitterCredentialses.ToArray();
            }
        }

        private static async Task DoUser(TwitterCredentials credentials)
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = credentials.ConsumerKey,
                    ConsumerSecret = credentials.ConsumerSecret,
                    AccessToken = credentials.AccessToken,
                    AccessTokenSecret = credentials.AccessTokenSecret
                }
            };
            var twitterCtx = new TwitterContext(auth);

            var searchResponse = await
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                       search.Query == "\"LINQ to Twitter\""
                 select search).SingleOrDefaultAsync();

            if (searchResponse != null && searchResponse.Statuses != null)
                searchResponse.Statuses.ForEach(tweet =>
                    Console.WriteLine(
                        "User: {0}, Tweet: {1}",
                        tweet.User.ScreenNameResponse,
                        tweet.Text));
        }
    }
}
