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
    public class Request : Model
    {
        [ForeignKey(nameof(IdentityUser))]
        public string UserID { get; set; } = null!;

        public DateTime ConfirmationDate { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Link { get; set; } = null!;

        [Required]
        public ResourceType Type { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public string Priority { get; set; } = null!;

        public string? RefusalReason { get; set; }

        [Required]
        public string Justification { get; set; } = null!;

        public IdentityUser IdentityUser { get; set; } = null!;
    }
}
