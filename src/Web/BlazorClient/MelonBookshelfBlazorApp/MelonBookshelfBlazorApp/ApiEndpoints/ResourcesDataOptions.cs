namespace MelonBookshelfBlazorApp.ApiEndpoints
{
    public class ResourcesDataOptions
    {
        public string Resources { get; set; } = default!;

        public string SearchResources { get; set; } = default!;

        public string GetResourcesCategories { get; set; } = default!;

        public string PhysicalResourceById { get; set; } = default!;

        public string ResourceById { get; set; } = default!;

        public string PhysicalResourcesByUserId { get; set; } = default!;
    }
}
