namespace LauncherServer.Migrations.LauncherDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgenres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "genre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "genre");
        }
    }
}
