namespace hangfire_template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSyncModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.t_time_entry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenProjectTimeEntryId = c.String(),
                        TrelloActionId = c.String(),
                        WorkPackageId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        SpentOn = c.DateTime(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_user", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.t_work_package", t => t.WorkPackageId, cascadeDelete: true)
                .Index(t => t.WorkPackageId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.t_time_entry", "WorkPackageId", "dbo.t_work_package");
            DropForeignKey("dbo.t_time_entry", "UserId", "dbo.t_user");
            DropIndex("dbo.t_time_entry", new[] { "UserId" });
            DropIndex("dbo.t_time_entry", new[] { "WorkPackageId" });
            DropTable("dbo.t_time_entry");
        }
    }
}
