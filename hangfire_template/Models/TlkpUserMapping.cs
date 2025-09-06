// Models/TlkpUserMapping.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hangfire_template.Models
{
    [Table("tlkp_user_mapping")]
    public class TlkpUserMapping
    {
        [Key]
        public int id { get; set; }

        [StringLength(100)]
        public string user_name { get; set; }

        [Required]
        [StringLength(150)]
        public string email { get; set; }

        [StringLength(100)]
        public string trello_member_id { get; set; }

        [StringLength(100)]
        public string openproject_user_id { get; set; }

        public bool is_active { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? last_synced_at { get; set; }
    }
}