using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class RequestUpvoter : Model
    {
        public int UserID { get; set; }
        public int ResourceID { get; set; }

        //public User User { get; set; }
        public Resource Resource { get; set; }
    }
}
