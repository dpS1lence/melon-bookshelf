using MelonBookchelfApi.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class Resource : Model
    {
        [Required]
        public ResourceType Type { get; set; }

        [ForeignKey(nameof(ResourceCategory))]
        public int CategoryID { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Link { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        public DateTime DateAdded { get; set; }

        public decimal PurchasePrice { get; set; }

        public string? InvoiceAttachment { get; set; }

        public string? ResourceDetails { get; set; }

        public ResourceCategory ResourceCategory { get; set; } = null!;
    }
}
