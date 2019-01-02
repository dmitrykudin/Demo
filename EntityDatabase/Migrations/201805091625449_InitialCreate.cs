namespace EntityDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductClasses", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.TagId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.ProductClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false),
                        ShortName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductClasses", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Rating = c.Decimal(precision: 18, scale: 2),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductClasses", t => t.ClassId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.ProductItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        PurchaseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CustomerId = c.Guid(nullable: false),
                        ShopId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PurchaseSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopId)
                .Index(t => t.CustomerId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Inn = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Products", "ClassId", "dbo.ProductClasses");
            DropForeignKey("dbo.ProductRatings", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Purchases", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ProductItems", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductRatings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ClassTags", "ClassId", "dbo.ProductClasses");
            DropForeignKey("dbo.ProductClasses", "ParentId", "dbo.ProductClasses");
            DropIndex("dbo.ProductRatings", new[] { "ProductId" });
            DropIndex("dbo.ProductRatings", new[] { "CustomerId" });
            DropIndex("dbo.Purchases", new[] { "ShopId" });
            DropIndex("dbo.Purchases", new[] { "CustomerId" });
            DropIndex("dbo.ProductItems", new[] { "PurchaseId" });
            DropIndex("dbo.ProductItems", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ClassId" });
            DropIndex("dbo.ProductClasses", new[] { "ParentId" });
            DropIndex("dbo.ClassTags", new[] { "ClassId" });
            DropIndex("dbo.ClassTags", new[] { "TagId" });
            DropTable("dbo.Tags");
            DropTable("dbo.Shops");
            DropTable("dbo.ProductRatings");
            DropTable("dbo.Customers");
            DropTable("dbo.Purchases");
            DropTable("dbo.ProductItems");
            DropTable("dbo.Products");
            DropTable("dbo.ProductClasses");
            DropTable("dbo.ClassTags");
        }
    }
}
