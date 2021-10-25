namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingEntities",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        CID = c.Int(nullable: false),
                        VId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.CounterDetailsEntities", t => t.CID, cascadeDelete: true)
                .ForeignKey("dbo.VendorDetailsEntities", t => t.VId, cascadeDelete: true)
                .Index(t => t.CID)
                .Index(t => t.VId);
            
            CreateTable(
                "dbo.CounterDetailsEntities",
                c => new
                    {
                        CounterID = c.Int(nullable: false, identity: true),
                        Availability = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CounterID);
            
            CreateTable(
                "dbo.VendorDetailsEntities",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        VendorName = c.String(maxLength: 20),
                        CounterName = c.String(maxLength: 20),
                        City = c.String(maxLength: 10),
                        Email = c.String(maxLength: 20),
                        ContactNo = c.String(maxLength: 10),
                        Pswd = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.VendorId);
            
            CreateTable(
                "dbo.MenuEntities",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        CounterID = c.Int(nullable: false),
                        ItemName = c.String(),
                        Itemdescription = c.String(maxLength: 30),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.CounterDetailsEntities", t => t.CounterID, cascadeDelete: true)
                .Index(t => t.CounterID);
            
            CreateTable(
                "dbo.UsersEntities",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 20),
                        ContactNo = c.String(maxLength: 10),
                        Email = c.String(maxLength: 20),
                        Pswd = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuEntities", "CounterID", "dbo.CounterDetailsEntities");
            DropForeignKey("dbo.BookingEntities", "VId", "dbo.VendorDetailsEntities");
            DropForeignKey("dbo.BookingEntities", "CID", "dbo.CounterDetailsEntities");
            DropIndex("dbo.MenuEntities", new[] { "CounterID" });
            DropIndex("dbo.BookingEntities", new[] { "VId" });
            DropIndex("dbo.BookingEntities", new[] { "CID" });
            DropTable("dbo.UsersEntities");
            DropTable("dbo.MenuEntities");
            DropTable("dbo.VendorDetailsEntities");
            DropTable("dbo.CounterDetailsEntities");
            DropTable("dbo.BookingEntities");
        }
    }
}
