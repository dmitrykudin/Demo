namespace EntityDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithAssociationRules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssociationRules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Confidence = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RuleConditions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RuleId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AssociationRules", t => t.RuleId, cascadeDelete: true)
                .Index(t => t.RuleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.RuleResults",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RuleId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AssociationRules", t => t.RuleId, cascadeDelete: true)
                .Index(t => t.RuleId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RuleResults", "RuleId", "dbo.AssociationRules");
            DropForeignKey("dbo.RuleConditions", "RuleId", "dbo.AssociationRules");
            DropForeignKey("dbo.RuleResults", "ProductId", "dbo.Products");
            DropForeignKey("dbo.RuleConditions", "ProductId", "dbo.Products");
            DropIndex("dbo.RuleResults", new[] { "ProductId" });
            DropIndex("dbo.RuleResults", new[] { "RuleId" });
            DropIndex("dbo.RuleConditions", new[] { "ProductId" });
            DropIndex("dbo.RuleConditions", new[] { "RuleId" });
            DropTable("dbo.RuleResults");
            DropTable("dbo.RuleConditions");
            DropTable("dbo.AssociationRules");
        }
    }
}
