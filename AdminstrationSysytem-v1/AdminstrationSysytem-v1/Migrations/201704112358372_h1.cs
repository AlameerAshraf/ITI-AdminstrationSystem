namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Student", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Student", new[] { "DepartmentId" });
            AlterColumn("dbo.Student", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Student", "DepartmentId");
            AddForeignKey("dbo.Student", "DepartmentId", "dbo.Departments", "DepartmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Student", new[] { "DepartmentId" });
            AlterColumn("dbo.Student", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Student", "DepartmentId");
            AddForeignKey("dbo.Student", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
        }
    }
}
