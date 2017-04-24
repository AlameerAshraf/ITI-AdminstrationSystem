namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Instructors", name: "Department_DepartmentId", newName: "DepartmentId");
            RenameIndex(table: "dbo.Instructors", name: "IX_Department_DepartmentId", newName: "IX_DepartmentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Instructors", name: "IX_DepartmentId", newName: "IX_Department_DepartmentId");
            RenameColumn(table: "dbo.Instructors", name: "DepartmentId", newName: "Department_DepartmentId");
        }
    }
}
