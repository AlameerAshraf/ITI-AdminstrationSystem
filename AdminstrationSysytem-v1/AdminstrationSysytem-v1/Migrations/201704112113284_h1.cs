namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CoursesDepartments", newName: "DepartmentsCourses");
            RenameColumn(table: "dbo.Qualifications", name: "QualificationName", newName: "Name");
            DropPrimaryKey("dbo.Qualifications");
            DropPrimaryKey("dbo.DepartmentsCourses");
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionBody = c.Int(nullable: false),
                        QuestionType = c.Int(nullable: false),
                        QuestionRightAnswer = c.Int(nullable: false),
                        CoursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Courses", t => t.CoursId, cascadeDelete: true)
                .Index(t => t.CoursId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ExamId = c.Int(nullable: false, identity: true),
                        StartsFrom = c.Int(nullable: false),
                        EndsIn = c.Int(nullable: false),
                        Subject = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExamId);
            
            CreateTable(
                "dbo.Course_Student_Exam",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CoursesId = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                        ExamGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CoursesId, t.ExamId })
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.CoursesId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Course_Student_Instructor",
                c => new
                    {
                        InstructorId = c.String(nullable: false, maxLength: 128),
                        CoursesId = c.Int(nullable: false),
                        StudentId = c.String(nullable: false, maxLength: 128),
                        InstructorEvaluation = c.Int(nullable: false),
                        StudentLabDegree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstructorId, t.CoursesId, t.StudentId })
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.InstructorId)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.InstructorId)
                .Index(t => t.CoursesId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        QuestionAnswer = c.String(name: "Question Answer", nullable: false, maxLength: 128),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionAnswer, t.QuestionId })
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        VacationId = c.Int(nullable: false, identity: true),
                        VacationName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VacationId);
            
            CreateTable(
                "dbo.ExamQuestions",
                c => new
                    {
                        Exam_ExamId = c.Int(nullable: false),
                        Questions_QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exam_ExamId, t.Questions_QuestionId })
                .ForeignKey("dbo.Exams", t => t.Exam_ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Questions_QuestionId, cascadeDelete: true)
                .Index(t => t.Exam_ExamId)
                .Index(t => t.Questions_QuestionId);
            
            CreateTable(
                "dbo.CoursesStudents",
                c => new
                    {
                        Courses_CoursId = c.Int(nullable: false),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Courses_CoursId, t.Student_Id })
                .ForeignKey("dbo.Courses", t => t.Courses_CoursId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Courses_CoursId)
                .Index(t => t.Student_Id);
            
            AddColumn("dbo.Student", "degree", c => c.Int(nullable: false));
            AddColumn("dbo.Qualifications", "InstructorId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Departments", "Name", c => c.String());
            AddPrimaryKey("dbo.Qualifications", new[] { "Name", "InstructorId" });
            AddPrimaryKey("dbo.DepartmentsCourses", new[] { "Departments_DepartmentId", "Courses_CoursId" });
            CreateIndex("dbo.Qualifications", "InstructorId");
            AddForeignKey("dbo.Qualifications", "InstructorId", "dbo.Instructors", "Id");
            DropColumn("dbo.Instructor_Corse_InDepartment", "InstructorEvaluation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instructor_Corse_InDepartment", "InstructorEvaluation", c => c.Int(nullable: false));
            DropForeignKey("dbo.QuestionAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Qualifications", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Course_Student_Instructor", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Course_Student_Instructor", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Course_Student_Instructor", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.Course_Student_Exam", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Course_Student_Exam", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Course_Student_Exam", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.CoursesStudents", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.CoursesStudents", "Courses_CoursId", "dbo.Courses");
            DropForeignKey("dbo.ExamQuestions", "Questions_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.ExamQuestions", "Exam_ExamId", "dbo.Exams");
            DropForeignKey("dbo.Questions", "CoursId", "dbo.Courses");
            DropIndex("dbo.CoursesStudents", new[] { "Student_Id" });
            DropIndex("dbo.CoursesStudents", new[] { "Courses_CoursId" });
            DropIndex("dbo.ExamQuestions", new[] { "Questions_QuestionId" });
            DropIndex("dbo.ExamQuestions", new[] { "Exam_ExamId" });
            DropIndex("dbo.QuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.Qualifications", new[] { "InstructorId" });
            DropIndex("dbo.Course_Student_Instructor", new[] { "StudentId" });
            DropIndex("dbo.Course_Student_Instructor", new[] { "CoursesId" });
            DropIndex("dbo.Course_Student_Instructor", new[] { "InstructorId" });
            DropIndex("dbo.Course_Student_Exam", new[] { "ExamId" });
            DropIndex("dbo.Course_Student_Exam", new[] { "CoursesId" });
            DropIndex("dbo.Course_Student_Exam", new[] { "StudentId" });
            DropIndex("dbo.Questions", new[] { "CoursId" });
            DropPrimaryKey("dbo.DepartmentsCourses");
            DropPrimaryKey("dbo.Qualifications");
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Qualifications", "InstructorId");
            DropColumn("dbo.Student", "degree");
            DropTable("dbo.CoursesStudents");
            DropTable("dbo.ExamQuestions");
            DropTable("dbo.Vacations");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.Course_Student_Instructor");
            DropTable("dbo.Course_Student_Exam");
            DropTable("dbo.Exams");
            DropTable("dbo.Questions");
            AddPrimaryKey("dbo.DepartmentsCourses", new[] { "Courses_CoursId", "Departments_DepartmentId" });
            AddPrimaryKey("dbo.Qualifications", "QualificationName");
            RenameColumn(table: "dbo.Qualifications", name: "Name", newName: "QualificationName");
            RenameTable(name: "dbo.DepartmentsCourses", newName: "CoursesDepartments");
        }
    }
}
