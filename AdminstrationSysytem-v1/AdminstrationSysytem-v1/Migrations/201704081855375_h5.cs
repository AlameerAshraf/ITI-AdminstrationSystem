namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "ManagerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Instructors", "ManagerId");
            AddForeignKey("dbo.Instructors", "ManagerId", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "ManagerId", "dbo.Instructors");
            DropIndex("dbo.Instructors", new[] { "ManagerId" });
            DropColumn("dbo.Instructors", "ManagerId");
        }
    }
}
