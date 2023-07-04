using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models.DTOs
{
    public class ResourceDetailsDto
    {
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime TakenDate { get; set; }
        public string DetailsLink { get; set; }
    }
}
