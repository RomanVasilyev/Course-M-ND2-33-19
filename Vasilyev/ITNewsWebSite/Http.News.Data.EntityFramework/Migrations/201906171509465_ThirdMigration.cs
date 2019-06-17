namespace Http.News.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Likes", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Likes", new[] { "UserId", "ItemId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Likes", "UserId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Likes", new[] { "UserId", "ItemId" });
        }
    }
}
