namespace LauncherServer.Migrations.LauncherDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Computers", "description", c => c.String());
            AddColumn("dbo.SteamUsers", "salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SteamUsers", "salt");
            DropColumn("dbo.Computers", "description");
        }
    }
}
