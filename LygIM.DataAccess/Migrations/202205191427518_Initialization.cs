namespace LygIM.DataAccess.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Action = c.String(nullable: false, maxLength: 200),
                        User = c.String(nullable: false, maxLength: 50),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Timestamp);
            
            CreateTable(
                "dbo.Workspaces",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.WorkspaceConfigurations",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        WorkspaceId = c.Guid(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workspaces", t => t.WorkspaceId, cascadeDelete: true)
                .Index(t => t.WorkspaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkspaceConfigurations", "WorkspaceId", "dbo.Workspaces");
            DropIndex("dbo.WorkspaceConfigurations", new[] { "WorkspaceId" });
            DropIndex("dbo.Workspaces", new[] { "Name" });
            DropIndex("dbo.Audits", new[] { "Timestamp" });
            DropTable("dbo.WorkspaceConfigurations");
            DropTable("dbo.Workspaces");
            DropTable("dbo.Audits");
        }
    }
}
