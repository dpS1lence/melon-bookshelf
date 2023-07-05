using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class RequestFollower
    {
        [ForeignKey(nameof(IdentityUser))]
        public string UserID { get; set; } = null!;

        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        public IdentityUser IdentityUser { get; set; } = null!;

        public Request Request { get; set; } = null!;
    }
}
