using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

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
                await PullTweetsForScreenName("whatsupanna", credentials);
                await PullReceivedDirectMessages(credentials);
                await PullSentDirectMessages(credentials);
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

        private static async Task PullTweetsForScreenName(string screenName, TwitterCredentials credentials)
        {
            var service = CreateTwitterService(credentials);

            var r = await service.ListTweetsOnUserTimelineAsync(new ListTweetsOnUserTimelineOptions
            {
                ScreenName = screenName,
                Count = 200,
                SinceId = 100
            });
            Console.WriteLine(r.Value == null ? 0 : r.Value.Count());
        }

        private static TwitterService CreateTwitterService(TwitterCredentials credentials)
        {
            var service = new TwitterService(credentials.ConsumerKey, credentials.ConsumerSecret);
            if (!string.IsNullOrEmpty(credentials.AccessToken) || !string.IsNullOrEmpty(credentials.AccessTokenSecret))
            {
                service.AuthenticateWith(credentials.AccessToken, credentials.AccessTokenSecret);
            }
            return service;
        }

        private static async Task PullReceivedDirectMessages(TwitterCredentials credentials)
        {
            var service = new TwitterService(credentials.ConsumerKey, credentials.ConsumerSecret);
            if (!string.IsNullOrEmpty(credentials.AccessToken) || !string.IsNullOrEmpty(credentials.AccessTokenSecret))
            {
                service.AuthenticateWith(credentials.AccessToken, credentials.AccessTokenSecret);
            }

            var r = await service.ListDirectMessagesReceivedAsync(new ListDirectMessagesReceivedOptions
            {
                Count = 200
            });
            Console.WriteLine(r.Value == null ? 0 : r.Value.Count());
        }

        private static async Task PullSentDirectMessages(TwitterCredentials credentials)
        {
            var service = new TwitterService(credentials.ConsumerKey, credentials.ConsumerSecret);
            if (!string.IsNullOrEmpty(credentials.AccessToken) || !string.IsNullOrEmpty(credentials.AccessTokenSecret))
            {
                service.AuthenticateWith(credentials.AccessToken, credentials.AccessTokenSecret);
            }

            var r = await service.ListDirectMessagesSentAsync(new ListDirectMessagesSentOptions
            {
                Count = 200
            });
            Console.WriteLine(r.Value == null ? 0 : r.Value.Count());
        }

    }
}
