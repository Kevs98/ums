using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Core.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? username {  get; set; }
        public int? role_id { get; set; }
        [ForeignKey("role_id")]
        public Role? Role { get; set; }
        public int? zone_id {  get; set; }
        [ForeignKey("zone_id")]
        public Zone? Zone { get; set; }
        public string? otpSecret { get; set; }
        public string? image { get; set; }
        public DateOnly birthDate { get; set; }
        public string? gender { get; set; }

        public ICollection<Applications>? ApplicationsApproved { get; set; }
        public ICollection<Applications>? ApplicationsApplicant { get; set; }


    }
}
