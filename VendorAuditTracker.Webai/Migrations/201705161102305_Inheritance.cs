namespace VendorAuditTracker.Webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        InternalSystemId = c.String(),
                        ExternalSystemId = c.String(),
                        SoftwareRelease_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SoftwareReleases", t => t.SoftwareRelease_Id)
                .Index(t => t.SoftwareRelease_Id);
            
            CreateTable(
                "dbo.CodeDrops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorName = c.String(),
                        TargetedDate = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.SoftwareReleases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Version = c.Long(nullable: false),
                        ReleaseName = c.String(),
                        TargetedDate = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PrimaryContact = c.String(),
                        SecondaryContact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VendorProjects",
                c => new
                    {
                        Vendor_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vendor_Id, t.Project_Id })
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.Vendor_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VendorProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.VendorProjects", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.Projects", "SoftwareRelease_Id", "dbo.SoftwareReleases");
            DropForeignKey("dbo.CodeDrops", "Project_Id", "dbo.Projects");
            DropIndex("dbo.VendorProjects", new[] { "Project_Id" });
            DropIndex("dbo.VendorProjects", new[] { "Vendor_Id" });
            DropIndex("dbo.CodeDrops", new[] { "Project_Id" });
            DropIndex("dbo.Projects", new[] { "SoftwareRelease_Id" });
            DropTable("dbo.VendorProjects");
            DropTable("dbo.Vendors");
            DropTable("dbo.SoftwareReleases");
            DropTable("dbo.CodeDrops");
            DropTable("dbo.Projects");
        }
    }
}
