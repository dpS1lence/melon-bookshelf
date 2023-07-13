using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class RequestUpvoter
    {
        [ForeignKey(nameof(IdentityUser))]
        public string UserID { get; set; } = null!;

        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        public IdentityUser IdentityUser { get; set; } = null!;

        public Request Request { get; set; } = null!;
    }
}
