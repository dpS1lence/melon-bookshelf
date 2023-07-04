using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonBookchelfApi.Infrastructure.Data.Models
{
    public class ResourceCategory : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
