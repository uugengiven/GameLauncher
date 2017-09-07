namespace LauncherServer.Migrations.LauncherDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addComputer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        ip = c.String(),
                        key = c.String(),
                        authorized = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.SteamUsers", "inUseBy_id", c => c.Int());
            CreateIndex("dbo.SteamUsers", "inUseBy_id");
            AddForeignKey("dbo.SteamUsers", "inUseBy_id", "dbo.Computers", "id");
            DropColumn("dbo.SteamUsers", "inUseBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SteamUsers", "inUseBy", c => c.String());
            DropForeignKey("dbo.SteamUsers", "inUseBy_id", "dbo.Computers");
            DropIndex("dbo.SteamUsers", new[] { "inUseBy_id" });
            DropColumn("dbo.SteamUsers", "inUseBy_id");
            DropTable("dbo.Computers");
        }
    }
}
