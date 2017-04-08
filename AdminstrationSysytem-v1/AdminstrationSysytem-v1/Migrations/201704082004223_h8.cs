namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CoursId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LectureDuration = c.Int(nullable: false),
                        LabDuration = c.Int(nullable: false),
                        TotalGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoursId);
            
            CreateTable(
                "dbo.InstructorsCourses",
                c => new
                    {
                        Instructors_Id = c.String(nullable: false, maxLength: 128),
                        Courses_CoursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructors_Id, t.Courses_CoursId })
                .ForeignKey("dbo.Instructors", t => t.Instructors_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Courses_CoursId, cascadeDelete: true)
                .Index(t => t.Instructors_Id)
                .Index(t => t.Courses_CoursId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstructorsCourses", "Courses_CoursId", "dbo.Courses");
            DropForeignKey("dbo.InstructorsCourses", "Instructors_Id", "dbo.Instructors");
            DropIndex("dbo.InstructorsCourses", new[] { "Courses_CoursId" });
            DropIndex("dbo.InstructorsCourses", new[] { "Instructors_Id" });
            DropTable("dbo.InstructorsCourses");
            DropTable("dbo.Courses");
        }
    }
}
