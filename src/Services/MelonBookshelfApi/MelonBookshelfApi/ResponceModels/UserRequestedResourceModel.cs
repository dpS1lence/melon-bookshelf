namespace MelonBookshelfApi.ResponceModels
{
    public class UserRequestedResourceModel
    {
        public string Status { get; set; } = null!;

        public string DeliveryStatus { get; set; }

        public string Category { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public string DateAdded { get; set; } = null!;

        public int UpvotersCount { get; set; }

        public int FollowersCount { get; set; }

        public IEnumerable<User> UpvotersCollection { get; set; } = null!;

        public IEnumerable<User> FollowersCollection { get; set; } = null!;
    }
}
