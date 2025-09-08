using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hangfire_template.Models
{
    [Table("t_work_package")]
    public class TWorkPackage
    {
        // DIUBAH: Menambahkan atribut [Column] untuk memetakan ke nama kolom yang benar
        [Key]
        [Column("id_work_package")]
        public int id_work_package { get; set; }

        [StringLength(255)]
        public string work_package_name { get; set; }

        public string description { get; set; }

        [StringLength(100)]
        public string work_package_id { get; set; }

        // Kode ini sudah benar sesuai screenshot Anda, jadi kita biarkan
        public bool is_synced { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? last_synced_at { get; set; }
    }
}