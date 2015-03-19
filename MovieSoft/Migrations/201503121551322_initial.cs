namespace MovieSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SoftMovies", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SoftMovies", "FileName");
        }
    }
}
