namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ExamQuestions", newName: "QuestionsExams");
            DropForeignKey("dbo.Course_Student_Exam", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.Course_Student_Exam", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Course_Student_Exam", "StudentId", "dbo.Student");
            DropIndex("dbo.Course_Student_Exam", new[] { "StudentId" });
            DropIndex("dbo.Course_Student_Exam", new[] { "CoursesId" });
            DropIndex("dbo.Course_Student_Exam", new[] { "ExamId" });
            DropPrimaryKey("dbo.QuestionsExams");
            CreateTable(
                "dbo.Student_Exam",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        ExamId = c.Int(nullable: false),
                        ExamGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.ExamId })
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId);
            
            AddColumn("dbo.Student", "Name", c => c.String());
            AddColumn("dbo.Exams", "course_CoursId", c => c.Int());
            AlterColumn("dbo.Instructors", "BD", c => c.DateTime());
            AddPrimaryKey("dbo.QuestionsExams", new[] { "Questions_QuestionId", "Exam_ExamId" });
            CreateIndex("dbo.Exams", "course_CoursId");
            AddForeignKey("dbo.Exams", "course_CoursId", "dbo.Courses", "CoursId");
            DropColumn("dbo.Student", "Fname");
            DropColumn("dbo.Student", "Lname");
            DropTable("dbo.Course_Student_Exam");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Course_Student_Exam",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CoursesId = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                        ExamGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CoursesId, t.ExamId });
            
            AddColumn("dbo.Student", "Lname", c => c.String());
            AddColumn("dbo.Student", "Fname", c => c.String());
            DropForeignKey("dbo.Student_Exam", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Student_Exam", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "course_CoursId", "dbo.Courses");
            DropIndex("dbo.Student_Exam", new[] { "ExamId" });
            DropIndex("dbo.Student_Exam", new[] { "StudentId" });
            DropIndex("dbo.Exams", new[] { "course_CoursId" });
            DropPrimaryKey("dbo.QuestionsExams");
            AlterColumn("dbo.Instructors", "BD", c => c.DateTime(nullable: false));
            DropColumn("dbo.Exams", "course_CoursId");
            DropColumn("dbo.Student", "Name");
            DropTable("dbo.Student_Exam");
            AddPrimaryKey("dbo.QuestionsExams", new[] { "Exam_ExamId", "Questions_QuestionId" });
            CreateIndex("dbo.Course_Student_Exam", "ExamId");
            CreateIndex("dbo.Course_Student_Exam", "CoursesId");
            CreateIndex("dbo.Course_Student_Exam", "StudentId");
            AddForeignKey("dbo.Course_Student_Exam", "StudentId", "dbo.Student", "Id");
            AddForeignKey("dbo.Course_Student_Exam", "ExamId", "dbo.Exams", "ExamId", cascadeDelete: true);
            AddForeignKey("dbo.Course_Student_Exam", "CoursesId", "dbo.Courses", "CoursId", cascadeDelete: true);
            RenameTable(name: "dbo.QuestionsExams", newName: "ExamQuestions");
        }
    }
}
