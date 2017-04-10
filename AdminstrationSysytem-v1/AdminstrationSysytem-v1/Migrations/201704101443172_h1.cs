namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h1 : DbMigration
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
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Capacity = c.Int(nullable: false),
                        ManagerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Instructors", t => t.ManagerId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Instructor_Corse_InDepartment",
                c => new
                    {
                        InstructorId = c.String(nullable: false, maxLength: 128),
                        CoursesId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        InstructorEvaluation = c.Int(nullable: false),
                        CourseStatues = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstructorId, t.CoursesId, t.DepartmentId })
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.InstructorId)
                .Index(t => t.InstructorId)
                .Index(t => t.CoursesId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.QualificationName);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Department_DepartmentId = c.Int(),
                        Name = c.String(),
                        Address = c.String(),
                        GraduationYear = c.String(),
                        Status = c.String(),
                        BD = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.Id)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Fname = c.String(),
                        Lname = c.String(),
                        Address = c.String(),
                        BD = c.DateTime(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Student", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Instructors", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Instructors", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Instructor_Corse_InDepartment", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Instructor_Corse_InDepartment", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Instructor_Corse_InDepartment", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.Instructors");
            DropForeignKey("dbo.InstructorsCourses", "Courses_CoursId", "dbo.Courses");
            DropForeignKey("dbo.InstructorsCourses", "Instructors_Id", "dbo.Instructors");
            DropForeignKey("dbo.DepartmentsCourses", "Courses_CoursId", "dbo.Courses");
            DropForeignKey("dbo.DepartmentsCourses", "Departments_DepartmentId", "dbo.Departments");
            DropIndex("dbo.Student", new[] { "DepartmentId" });
            DropIndex("dbo.Student", new[] { "Id" });
            DropIndex("dbo.Instructors", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Instructors", new[] { "Id" });
            DropIndex("dbo.InstructorsCourses", new[] { "Courses_CoursId" });
            DropIndex("dbo.InstructorsCourses", new[] { "Instructors_Id" });
            DropIndex("dbo.DepartmentsCourses", new[] { "Courses_CoursId" });
            DropIndex("dbo.DepartmentsCourses", new[] { "Departments_DepartmentId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Instructor_Corse_InDepartment", new[] { "DepartmentId" });
            DropIndex("dbo.Instructor_Corse_InDepartment", new[] { "CoursesId" });
            DropIndex("dbo.Instructor_Corse_InDepartment", new[] { "InstructorId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropTable("dbo.Student");
            DropTable("dbo.Instructors");
            DropTable("dbo.InstructorsCourses");
            DropTable("dbo.DepartmentsCourses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Qualifications");
            DropTable("dbo.Instructor_Corse_InDepartment");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
        }
    }
}
