using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class Request : Model
    {
        public int RequestID { get; set; }
        public int ResourceID { get; set; }
        public int UserID { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Justification { get; set; }
        public string Reason { get; set; }

        public Resource Resource { get; set; }
        //public User User { get; set; }
    }
}
