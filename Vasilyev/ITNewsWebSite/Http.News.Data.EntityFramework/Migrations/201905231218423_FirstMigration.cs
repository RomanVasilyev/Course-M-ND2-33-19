namespace Http.News.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemContentId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.ItemContents", t => t.ItemContentId, cascadeDelete: true)
                .Index(t => t.ItemContentId)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ItemContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Content = c.String(),
                        SmallImage = c.String(),
                        MediumImage = c.String(),
                        BigImage = c.String(),
                        NumOfView = c.Long(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ItemContentId", "dbo.ItemContents");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropIndex("dbo.Items", new[] { "ItemContentId" });
            DropTable("dbo.ItemContents");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
