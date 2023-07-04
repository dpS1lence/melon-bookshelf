using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class Resource : Model
    {
        [Required]
        public ResourceType Type { get; set; }
        public int CategoryID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal PurchasePrice { get; set; }
        public string? InvoiceAttachment { get; set; }
        public string? ResourceDetails { get; set; }
        public ResourceCategory ResourceCategory { get; set; }
    }
}
