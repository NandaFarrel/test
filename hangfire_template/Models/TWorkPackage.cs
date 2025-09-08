using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hangfire_template.Models
{
    [Table("t_work_package")]
    public class TWorkPackage
    {
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        public string work_package_name { get; set; }
        public string description { get; set; }
        [StringLength(100)]
        public string work_package_id { get; set; }
        public bool is_synced { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? last_synced_at { get; set; }
    }
}