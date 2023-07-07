using MelonBookchelfApi.Infrastructure.Data.Models.Enums;

namespace MelonBookshelfApi.RequestDtos
{
    public class ResourceRequestDto
    {
        public string? UserId { get; set; }
        public string Category { get; set; }
        public ResourceType Type { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Link { get; set; }
        public string Priority { get; set; }
        public string Justification { get; set; }
    }
}
