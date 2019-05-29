namespace Http.News.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "TotalRaters", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "AverageRating", c => c.Double(nullable: false));
            DropColumn("dbo.Items", "ItmRaiting_Rating");
            DropColumn("dbo.Items", "ItmRaiting_TotalRaters");
            DropColumn("dbo.Items", "ItmRaiting_AverageRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ItmRaiting_AverageRating", c => c.Double(nullable: false));
            AddColumn("dbo.Items", "ItmRaiting_TotalRaters", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ItmRaiting_Rating", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "AverageRating");
            DropColumn("dbo.Items", "TotalRaters");
            DropColumn("dbo.Items", "Rating");
        }
    }
}
