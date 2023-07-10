namespace MelonBookshelfBlazorApp.ApiEndpoints
{
    public class BaseUserActionsOptions
    {
        public string ReturnPhysicalResource { get; set; } = default!;

        public string GetPhysicalResource { get; set; } = default!;

        public string UpvoteRequest { get; set; } = default!;

        public string FollowRequest { get; set; } = default!;
    }
}
