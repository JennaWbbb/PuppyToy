using PuppyToy.Models.Enum;

namespace PuppyToy.Models.Storable {
    public class EmlalockFeedItem : BaseStorable {

        public string? Title {
            get;
            set;
        }

        public DateTime PupDate {
            get;
            set;
        }

        public string? ExternalId {
            get;
            set;
        }

        public LockActionType ActionType {
            get;
            set;
        }

        public EmlalockSession Session {
            get;
            set;
        }

        public int TimeDelta {
            get;
            set;
        }

        public string? TimeUnit {
            get;
            set;
        }

        public int TimeDeltaSeconds {
            get {
                if( string.IsNullOrEmpty( TimeUnit ) ) {
                    return 0;
                }
                if( TimeUnit.Contains( "Minute", StringComparison.CurrentCultureIgnoreCase ) ) {
                    return TimeDelta * 60;
                }
                if( TimeUnit.Contains( "Hour", StringComparison.CurrentCultureIgnoreCase ) ) {
                    return TimeDelta * 60 * 60;
                }
                if( TimeUnit.Contains( "Day", StringComparison.CurrentCultureIgnoreCase ) ) {
                    return TimeDelta * 60 * 60 * 24;
                }
                if( TimeUnit.Contains( "Week", StringComparison.CurrentCultureIgnoreCase ) ) {
                    return TimeDelta * 60 * 60 * 24 * 7;
                }
                return 0;
            }
        }

        public override string ToString() {
            return $"[{PupDate:s}] [{ActionType}] {Title}";
        }
    }
}
