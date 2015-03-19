namespace MovieSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieGenres", "SoftMovie_ID", c => c.Int());
            CreateIndex("dbo.MovieGenres", "SoftMovie_ID");
            AddForeignKey("dbo.MovieGenres", "SoftMovie_ID", "dbo.SoftMovies", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieGenres", "SoftMovie_ID", "dbo.SoftMovies");
            DropIndex("dbo.MovieGenres", new[] { "SoftMovie_ID" });
            DropColumn("dbo.MovieGenres", "SoftMovie_ID");
        }
    }
}
