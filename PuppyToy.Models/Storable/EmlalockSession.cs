namespace PuppyToy.Models.Storable {
    public class EmlalockSession : BaseStorable {

        public List<EmlalockFeedItem> FeedItems {
            get; set;
        } = new List<EmlalockFeedItem>();

        public string SessionId {
            get;
            set;
        }

    }
}
