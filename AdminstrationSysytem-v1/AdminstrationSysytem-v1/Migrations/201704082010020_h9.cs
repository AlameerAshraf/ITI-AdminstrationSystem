namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentsCourses",
                c => new
                    {
                        Departments_DepartmentId = c.Int(nullable: false),
                        Courses_CoursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Departments_DepartmentId, t.Courses_CoursId })
                .ForeignKey("dbo.Departments", t => t.Departments_DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Courses_CoursId, cascadeDelete: true)
                .Index(t => t.Departments_DepartmentId)
                .Index(t => t.Courses_CoursId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentsCourses", "Courses_CoursId", "dbo.Courses");
            DropForeignKey("dbo.DepartmentsCourses", "Departments_DepartmentId", "dbo.Departments");
            DropIndex("dbo.DepartmentsCourses", new[] { "Courses_CoursId" });
            DropIndex("dbo.DepartmentsCourses", new[] { "Departments_DepartmentId" });
            DropTable("dbo.DepartmentsCourses");
        }
    }
}
