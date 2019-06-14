namespace Http.News.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ItemId = c.Int(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ItemId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            AddColumn("dbo.Items", "TotalLikes", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "TotalDislikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "ItemId", "dbo.Items");
            DropIndex("dbo.Likes", new[] { "ItemId" });
            DropColumn("dbo.Items", "TotalDislikes");
            DropColumn("dbo.Items", "TotalLikes");
            DropTable("dbo.Likes");
        }
    }
}
