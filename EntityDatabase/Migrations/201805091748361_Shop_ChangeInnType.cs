namespace EntityDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shop_ChangeInnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shops", "Inn", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shops", "Inn", c => c.Int(nullable: false));
        }
    }
}
