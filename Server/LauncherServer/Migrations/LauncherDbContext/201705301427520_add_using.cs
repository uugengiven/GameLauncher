namespace LauncherServer.Migrations.LauncherDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_using : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SteamUsers", "inUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.SteamUsers", "inUseBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SteamUsers", "inUseBy");
            DropColumn("dbo.SteamUsers", "inUse");
        }
    }
}
