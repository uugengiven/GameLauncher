namespace LauncherServer.Migrations.LauncherDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecretToComputers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Computers", "secret", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Computers", "secret");
        }
    }
}
