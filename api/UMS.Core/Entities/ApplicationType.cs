using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Core.Entities
{
    public class ApplicationType
    {
        [Key]
        public int type_id { get; set; }
        public string? type { get; set; }

        public ICollection<Applications>? Applications { get; set; }
    }
}
