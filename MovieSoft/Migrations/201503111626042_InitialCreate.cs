namespace MovieSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SoftMovies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        BackdropUrl = c.String(),
                        ImageUrl = c.String(),
                        VoteCount = c.Int(nullable: false),
                        VoteAverage = c.Double(nullable: false),
                        ReleaseDate = c.DateTime(),
                        Overview = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SoftMovies");
        }
    }
}
