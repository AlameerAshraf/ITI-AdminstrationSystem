namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instructors", "ManagerId", "dbo.Instructors");
            DropIndex("dbo.Instructors", new[] { "ManagerId" });
            AddColumn("dbo.Departments", "ManagerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Departments", "ManagerId");
            AddForeignKey("dbo.Departments", "ManagerId", "dbo.Instructors", "Id");
            DropColumn("dbo.Instructors", "ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instructors", "ManagerId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.Instructors");
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropColumn("dbo.Departments", "ManagerId");
            CreateIndex("dbo.Instructors", "ManagerId");
            AddForeignKey("dbo.Instructors", "ManagerId", "dbo.Instructors", "Id");
        }
    }
}
