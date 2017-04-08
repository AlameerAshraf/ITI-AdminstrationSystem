namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "Department_DepartmentId", c => c.Int());
            CreateIndex("dbo.Instructors", "Department_DepartmentId");
            AddForeignKey("dbo.Instructors", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructors", "Department_DepartmentId", "dbo.Departments");
            DropIndex("dbo.Instructors", new[] { "Department_DepartmentId" });
            DropColumn("dbo.Instructors", "Department_DepartmentId");
        }
    }
}
