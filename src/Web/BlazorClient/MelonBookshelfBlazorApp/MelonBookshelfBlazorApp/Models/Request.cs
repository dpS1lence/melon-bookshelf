using System.ComponentModel.DataAnnotations;

namespace MelonBookshelfBlazorApp.Models
{
	public class Request
	{
		public int Id { get; set; }
		public string? UserId { get; set; }
		public string? UserName { get; set; }
		public string Category { get; set; }
		public string Type { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public string DeliveryStatus { get; set; }
		public string Link { get; set; }
		public string Priority { get; set; }
		public string Justification { get; set; }
	}
}
