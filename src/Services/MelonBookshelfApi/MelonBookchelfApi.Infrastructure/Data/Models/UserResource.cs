using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class UserResource
    {
        [ForeignKey(nameof(IdentityUser))]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }

        public IdentityUser IdentityUser { get; set; } = null!;

        public Resource Resource { get; set; } = null!;
    }
}
