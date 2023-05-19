namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Guitars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guitars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                        NumberOfStrings = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        MusicianId = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Musicians", t => t.MusicianId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.MusicianId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Musicians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guitars", "MusicianId", "dbo.Musicians");
            DropForeignKey("dbo.Guitars", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Guitars", new[] { "MusicianId" });
            DropIndex("dbo.Guitars", new[] { "CategoryId" });
            DropTable("dbo.Musicians");
            DropTable("dbo.Categories");
            DropTable("dbo.Guitars");
        }
    }
}
