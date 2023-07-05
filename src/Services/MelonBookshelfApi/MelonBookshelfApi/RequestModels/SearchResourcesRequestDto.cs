namespace MelonBookshelfApi.RequestModels
{
    public class SearchResourcesRequestDto
    {
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Title { get; set; }
    }
}
