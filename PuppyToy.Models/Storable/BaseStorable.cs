namespace PuppyToy.Models.Storable {
    public class BaseStorable {

        public Guid Id {
            get; set;
        }

        public BaseStorable() {
            Id = Guid.NewGuid();
        }

    }
}
