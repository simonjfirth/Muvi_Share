namespace MovieSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbIDcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SoftMovies", "MovieDBID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SoftMovies", "MovieDBID");
        }
    }
}
