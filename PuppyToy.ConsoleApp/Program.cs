using PuppyToy.Models.Enum;
using PuppyToy.Models.Storable;
using PuppyToy.Services.Emlalock;
using System.Diagnostics;

namespace PuppyToy.ConsoleApp {
    internal abstract class Program {

        public static EmlalockSession CurrentSession {
            get;
            set;
        }

        public static void Main() {
            Console.WriteLine( "Enter feed URL: " );
            string? url = Console.ReadLine();
            Debug.WriteLine( $"URL: {url}" );
            Run( url );
        }

        private static void Run( string? url ) {
            EmlalockFeedReader reader = new EmlalockFeedReader( url );
            Dictionary<string, EmlalockFeedItem> cache = new Dictionary<string, EmlalockFeedItem>();
            DateTime lastSuccessfulFetch = DateTime.Now;

            while( true ) {
                try {
                    List<EmlalockFeedItem> result = reader.GetFeedItems().Result;
                    foreach( EmlalockFeedItem emlalockFeedItem in result ) {
                        cache.TryAdd( emlalockFeedItem.ExternalId, emlalockFeedItem );
                    }
                    lastSuccessfulFetch = DateTime.Now;
                } catch( Exception ex ) {
                    Debug.WriteLine( ex );
                }

                Console.Clear();

                Console.WriteLine( $"Time Fetched: {lastSuccessfulFetch:G}\n" );

                foreach( IGrouping<LockActionType, EmlalockFeedItem> emlalockFeedItems in cache.Values.GroupBy( r => r.ActionType ) ) {
                    TimeSpan timeSum = TimeSpan.FromSeconds( emlalockFeedItems.Sum( fi => fi.TimeDeltaSeconds ) );
                    Console.WriteLine( $"Count {emlalockFeedItems.Key}: {emlalockFeedItems.Count()}, Total Timespan: {timeSum:g}" );
                }

                int totalAdded = cache.Values.Where( r => r.TimeDelta >= 0 ).Sum( di => di.TimeDeltaSeconds );
                int totalRemoved = cache.Values.Where( r => r.TimeDelta < 0 ).Sum( di => di.TimeDeltaSeconds );

                TimeSpan totalDelta = TimeSpan.FromSeconds( Math.Abs( totalAdded ) - Math.Abs( totalRemoved ) );

                Console.WriteLine();

                Console.WriteLine( $"Session Start: {cache.Values.FirstOrDefault( v => v.ActionType == LockActionType.SessionStart )?.PupDate:g}" );
                Console.WriteLine( $"Total time added: {totalDelta:g}" );
                Thread.Sleep( 5000 );
            }
        }
    }
}
