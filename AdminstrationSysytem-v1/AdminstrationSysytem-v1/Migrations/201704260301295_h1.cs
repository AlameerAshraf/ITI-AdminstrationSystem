namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "NoOfAbsenceDay", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "NoOfAbsenceDay");
        }
    }
}
