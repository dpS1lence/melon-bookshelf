using System.Security.AccessControl;

namespace MelonBookshelfBlazorApp.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Author { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string? Link { get; set; }

        public string Status { get; set; } = default!;

        public DateTime DateAdded { get; set; }

        public double PurchasePrice { get; set; }

        public string? InvoiceAttachment { get; set; }

        public string? ResourceDetails { get; set; }

        public CategoryModel ResourceCategory { get; set; } = default!;
    }
}
