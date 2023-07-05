using MelonBookchelfApi.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MelonBookchelfApi.Infrastructure.Data.Models.Enums;

namespace MelonBookshelfApi.ResponceModels
{
    public class SearchResult
    {
        public ResourceType Type { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Link { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime DateAdded { get; set; }

        public double PurchasePrice { get; set; }

        public string? InvoiceAttachment { get; set; }

        public string? ResourceDetails { get; set; }

        public ResourceCategory ResourceCategory { get; set; } = null!;
    }
}
