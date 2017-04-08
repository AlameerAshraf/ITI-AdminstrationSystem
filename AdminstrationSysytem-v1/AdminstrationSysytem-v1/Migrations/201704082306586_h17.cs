namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instructor_Corse_InDepartment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstructorId = c.String(maxLength: 128),
                        CoursesId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.InstructorId)
                .Index(t => t.InstructorId)
                .Index(t => t.CoursesId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instructor_Corse_InDepartment", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Instructor_Corse_InDepartment", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Instructor_Corse_InDepartment", "CoursesId", "dbo.Courses");
            DropIndex("dbo.Instructor_Corse_InDepartment", new[] { "DepartmentId" });
            DropIndex("dbo.Instructor_Corse_InDepartment", new[] { "CoursesId" });
            DropIndex("dbo.Instructor_Corse_InDepartment", new[] { "InstructorId" });
            DropTable("dbo.Instructor_Corse_InDepartment");
        }
    }
}
