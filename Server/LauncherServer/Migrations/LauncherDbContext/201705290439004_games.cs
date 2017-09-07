namespace LauncherServer.Migrations.LauncherDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        steamId = c.Int(nullable: false),
                        name = c.String(),
                        exe = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SteamUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SteamUserGames",
                c => new
                    {
                        SteamUser_id = c.Int(nullable: false),
                        Game_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SteamUser_id, t.Game_id })
                .ForeignKey("dbo.SteamUsers", t => t.SteamUser_id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_id, cascadeDelete: true)
                .Index(t => t.SteamUser_id)
                .Index(t => t.Game_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SteamUserGames", "Game_id", "dbo.Games");
            DropForeignKey("dbo.SteamUserGames", "SteamUser_id", "dbo.SteamUsers");
            DropIndex("dbo.SteamUserGames", new[] { "Game_id" });
            DropIndex("dbo.SteamUserGames", new[] { "SteamUser_id" });
            DropTable("dbo.SteamUserGames");
            DropTable("dbo.SteamUsers");
            DropTable("dbo.Games");
        }
    }
}
