using System.ComponentModel.DataAnnotations;

namespace MelonBookshelfBlazorApp.Models
{
	public class Request
	{
		public int Id { get; set; }
		public DateTime ConfirmationDate { get; set; }
		public string Category { get; set; } = null!;
		public string UserName { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Author { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Link { get; set; } = null!;
		public string Type { get; set; }
		public string Status { get; set; } = null!;
		public string DeliveryStatus { get; set; } = null!;
		public string Priority { get; set; } = null!;
		public string? RefusalReason { get; set; }
		public string Justification { get; set; } = null!;
	}
}
