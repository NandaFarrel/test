namespace hangfire_template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterWorkPackageTableForRelations : DbMigration
    {
        public override void Up()
        {
            // --- Langkah 1: Hapus kolom lama yang tidak diperlukan lagi ---
            DropColumn("dbo.t_work_package", "openproject_project_id");
            DropColumn("dbo.t_work_package", "is_synced");

            // --- Langkah 2: Ganti nama kolom yang sudah ada agar sesuai model baru ---
            RenameColumn(table: "dbo.t_work_package", name: "work_package_id", newName: "OpenProjectWorkPackageId");
            RenameColumn(table: "dbo.t_work_package", name: "trello_card_id", newName: "TrelloCardId");
            RenameColumn(table: "dbo.t_work_package", name: "work_package_name", newName: "Name");
            RenameColumn(table: "dbo.t_work_package", name: "description", newName: "Description");
            RenameColumn(table: "dbo.t_work_package", name: "created_at", newName: "CreatedAt");
            RenameColumn(table: "dbo.t_work_package", name: "last_synced_at", newName: "LastSyncedAt");
            RenameColumn(table: "dbo.t_work_package", name: "needs_op_sync", newName: "NeedsOpSync");

            // --- Langkah 3: Tambahkan kolom baru untuk relasi dan fitur baru ---
            AddColumn("dbo.t_work_package", "ProjectId", c => c.Int());
            AddColumn("dbo.t_work_package", "StatusId", c => c.Int());
            AddColumn("dbo.t_work_package", "AssigneeId", c => c.Int());
            AddColumn("dbo.t_work_package", "DueDate", c => c.DateTime());
            AddColumn("dbo.t_work_package", "NeedsTrelloSync", c => c.Boolean(nullable: false, defaultValue: false));

            // --- Langkah 4: Ubah tipe data kolom yang diperlukan ---
            AlterColumn("dbo.t_work_package", "OpenProjectWorkPackageId", c => c.String(maxLength: 450));
            AlterColumn("dbo.t_work_package", "TrelloCardId", c => c.String(maxLength: 450));
            AlterColumn("dbo.t_work_package", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.t_work_package", "LastSyncedAt", c => c.DateTime(nullable: false));

            // --- Langkah 5: Buat Index dan Foreign Key ---

            // PERBAIKAN: Menggunakan SQL langsung untuk membuat FILTERED UNIQUE INDEX
            Sql("CREATE UNIQUE NONCLUSTERED INDEX IX_OPWorkPackageId ON dbo.t_work_package(OpenProjectWorkPackageId) WHERE OpenProjectWorkPackageId IS NOT NULL;");
            Sql("CREATE UNIQUE NONCLUSTERED INDEX IX_TrelloCardId ON dbo.t_work_package(TrelloCardId) WHERE TrelloCardId IS NOT NULL;");

            CreateIndex("dbo.t_work_package", "ProjectId");
            CreateIndex("dbo.t_work_package", "StatusId");
            CreateIndex("dbo.t_work_package", "AssigneeId");
            AddForeignKey("dbo.t_work_package", "AssigneeId", "dbo.t_user", "Id");
            AddForeignKey("dbo.t_work_package", "ProjectId", "dbo.t_project", "Id");
            AddForeignKey("dbo.t_work_package", "StatusId", "dbo.t_status", "Id");
        }

        public override void Down()
        {
            DropIndex("dbo.t_work_package", new[] { "TrelloCardId" });
            DropIndex("dbo.t_work_package", "IX_OPWorkPackageId");
            AlterColumn("dbo.t_work_package", "TrelloCardId", c => c.String());
            AlterColumn("dbo.t_work_package", "OpenProjectWorkPackageId", c => c.String());
            CreateIndex("dbo.t_work_package", "TrelloCardId", unique: true);
            CreateIndex("dbo.t_work_package", "OpenProjectWorkPackageId", unique: true, name: "IX_WorkPackageId");
        }
    }
}
