using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hangfire_template.Models
{
    [Table("t_sync_log")]
    public class TSyncLog
    {
        [Key]
        public int log_id { get; set; }

        public string source_platform { get; set; } // "Trello" atau "OpenProject"
        public string event_type { get; set; } // Contoh: "cardCreated", "workPackageUpdated"
        public string source_item_id { get; set; } // ID dari Trello Card atau OP Work Package
        public DateTime synced_at { get; set; }
        public string details { get; set; } // Untuk menyimpan payload atau detail error
    }
}