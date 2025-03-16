using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Core.Entities
{
    public class Role
    {
        [Key]
        public int? role_id {  get; set; }
        public string? role {  get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
