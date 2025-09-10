// File: TWorkPackage.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hangfire_template.Models
{
    [Table("t_work_package")]
    public class TWorkPackage
    {
        [Key]
        [Column("id_work_package")]
        public int id { get; set; }

        // PERBAIKAN: Ubah dari string menjadi int agar cocok dengan database
        public int work_package_id { get; set; }

        public string trello_card_id { get; set; }
        public string work_package_name { get; set; }
        public string description { get; set; }
        public bool is_synced { get; set; }
        public DateTime? last_synced_at { get; set; }
        public DateTime? created_at { get; set; }
    }
}