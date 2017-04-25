namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "NoOfPermissions", c => c.Int());
            DropColumn("dbo.Attendances", "NoOfPermissions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "NoOfPermissions", c => c.Int(nullable: false));
            DropColumn("dbo.Student", "NoOfPermissions");
        }
    }
}
