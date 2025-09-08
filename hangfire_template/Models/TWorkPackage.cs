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

        // Kolom untuk ID dari OpenProject
        public string work_package_id { get; set; }

        // Kolom untuk ID dari Trello (BARU DITAMBAHKAN)
        public string trello_card_id { get; set; }

        // Kolom umum
        public string work_package_name { get; set; }
        public string description { get; set; }
        public bool is_synced { get; set; }
        public DateTime? last_synced_at { get; set; }
    }
}