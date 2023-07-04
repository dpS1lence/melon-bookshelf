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
        [ForeignKey(nameof(Resource))]
        public int ResourceID { get; set; }

        [ForeignKey(nameof(IdentityUser))]
        public string UserID { get; set; }

        public DateTime ConfirmationDate { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public string Priority { get; set; } = null!;

        [Required]
        public string Justification { get; set; } = null!;

        [Required]
        public string Reason { get; set; } = null!;

        public Resource Resource { get; set; } = null!;

        public IdentityUser IdentityUser { get; set; } = null!;
    }
}
