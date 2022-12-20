using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using PuppyToy.Models.Enum;
using PuppyToy.Models.Storable;

namespace PuppyToy.Services.Emlalock {
    public class EmlalockFeedReader {

        private readonly string? _feedUrl;
        private readonly HttpClient _emlaClient;

        private readonly string _timePattern = @"[\+\-]{1}[0-9]+ ((Minute[s]?)|(Hour[s]?)|(Day[s]?)|(Week[s]?))";

        public EmlalockFeedReader( string? feedUrl ) {
            _feedUrl = feedUrl;
            _emlaClient = new HttpClient();
        }

        public async Task<List<EmlalockFeedItem>> GetFeedItems() {
            List<EmlalockFeedItem> items = new List<EmlalockFeedItem>();

            HttpResponseMessage httpResponse = await _emlaClient.GetAsync( _feedUrl );
            if( httpResponse.IsSuccessStatusCode ) {
                Stream feedText = await httpResponse.Content.ReadAsStreamAsync();

                var feed = XElement.Load( feedText, LoadOptions.None );

                var rawFeedItems = feed.XPathSelectElements( "/channel/item" );

                foreach( XElement xElement in rawFeedItems ) {
                    EmlalockFeedItem feedItem = new EmlalockFeedItem();
                    feedItem.Title = xElement.Descendants( "title" ).Select( i => i.Value ).FirstOrDefault()?.ToString();
                    feedItem.ExternalId = xElement.Descendants( "guid" ).Select( i => i.Value ).FirstOrDefault()?.ToString();

                    DateTime.TryParse( xElement.Descendants( "pubDate" ).Select( i => i.Value ).FirstOrDefault()?.ToString(), out DateTime dateTime );
                    feedItem.PupDate = dateTime;

                    feedItem.ActionType = GetLockActionTypeFromTitle( feedItem.Title );

                        if( feedItem.Title != null ) {
                            string timeString = Regex.Match( feedItem.Title, _timePattern ).ToString();
                            if( !string.IsNullOrEmpty( timeString ) ) {
                                feedItem.TimeDelta = GetTimeDeltaAndUnitFromString( timeString ).Time;
                                feedItem.TimeUnit = GetTimeDeltaAndUnitFromString( timeString ).Unit;
                            }
                        }

                    items.Add( feedItem );
                }

            }

            return items;
        }

        private (int Time, string Unit) GetTimeDeltaAndUnitFromString( string timeString ) {
            string[] stringParts = timeString.Split( " " );
            int.TryParse( stringParts[0], out int time );

            return (time, stringParts[1]);
        }

        private LockActionType GetLockActionTypeFromTitle( string? title ) {
            if( string.IsNullOrEmpty( title ) || string.IsNullOrWhiteSpace( title ) ) {
                return LockActionType.Invalid;
            }

            if( title.Contains( "started a session" ) ) {
                return LockActionType.SessionStart;
            } else if( title.Contains( "Friend Link" ) || title.Contains( "voting from" ) ) {
                if( title.Contains( "(+" ) ) {
                    return LockActionType.VoteAdded;
                } else if( title.Contains( "(-" ) ) {
                    return LockActionType.VoteRemoved;
                }
            } else if( title.Contains( "because of a Pillory vote" ) ) {
                return LockActionType.PilloryAdded;
            } else if( title.Contains( "is now in Pillory" ) ) {
                return LockActionType.PilloryEntered;
            } else if( title.Contains( "because of the Requirement Link" ) ) {
                return LockActionType.RequirementLink;
            } else if( title.Contains( "played Wheel of Fortune" ) ) {
                return LockActionType.WheelOfFortune;
            } else if( title.Contains( "increased with the EmlaLock API" ) ) {
                return LockActionType.ApiAdded;
            } else if( title.Contains( "decreased with the EmlaLock API" ) ) {
                return LockActionType.ApiRemoved;
            }

            return LockActionType.Invalid;
        }
    }
}
