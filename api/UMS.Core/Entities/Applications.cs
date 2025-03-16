using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Core.Entities
{
    public class Applications
    {
        [Key]
        public int? id { get; set; }
        public int? type_id { get; set; }
        [ForeignKey("type_id")]
        public ApplicationType? type { get; set; }
        public int? zone_id { get; set; }
        [ForeignKey("zone_id")]
        public Zone? zone { get; set; }
        public string? observations {  get; set; }
        public int? approver_id {  get; set; }
        [ForeignKey(nameof(approver_id))]
        public User? approver { get; set; }
        public int? applicant_id { get; set; }
        [ForeignKey(nameof(applicant_id))]
        public User? applicant { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool? approved { get; set; }

    }
}
