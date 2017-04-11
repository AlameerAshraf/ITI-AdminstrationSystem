namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Student", "degree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Student", "degree", c => c.Int(nullable: false));
        }
    }
}
