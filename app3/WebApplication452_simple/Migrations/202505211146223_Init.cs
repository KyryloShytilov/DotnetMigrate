namespace WebApplication452_simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Birthdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CountryId = c.Int(nullable: false),
                        City = c.String(nullable: false, maxLength: 50),
                        Stars = c.Int(nullable: false),
                        PricePerNight = c.Double(nullable: false),
                        IsAllInclusive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOrdered = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NumberOfDays = c.Int(nullable: false),
                        FullPrice = c.Double(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                        Hotel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.Hotel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Hotels", "CountryId", "dbo.Countries");
            DropIndex("dbo.Orders", new[] { "Hotel_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Hotels", new[] { "CountryId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Hotels");
            DropTable("dbo.Customers");
            DropTable("dbo.Countries");
        }
    }
}
