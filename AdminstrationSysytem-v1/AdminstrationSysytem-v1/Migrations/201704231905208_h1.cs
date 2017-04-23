namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendances", "ArrivalTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attendances", "ArrivalTime", c => c.DateTime(nullable: false));
        }
    }
}
