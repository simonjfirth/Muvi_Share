namespace MovieSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genrestuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieID = c.Int(nullable: false),
                        Genre_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.Genre_ID)
                .Index(t => t.Genre_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieGenres", "Genre_ID", "dbo.Genres");
            DropIndex("dbo.MovieGenres", new[] { "Genre_ID" });
            DropTable("dbo.MovieGenres");
            DropTable("dbo.Genres");
        }
    }
}
