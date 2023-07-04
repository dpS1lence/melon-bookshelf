using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data
{
    public class DbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext(string connectionString)
           : base(connectionString)
        {
        }
    }
}
