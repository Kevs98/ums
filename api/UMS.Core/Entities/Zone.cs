using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Core.Entities
{
    public class Zone
    {
        [Key]
        public int? zone_id {  get; set; }
        public string? zone { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<Applications>? Applications { get; set; }
    }
}
