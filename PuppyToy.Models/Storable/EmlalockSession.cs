using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
