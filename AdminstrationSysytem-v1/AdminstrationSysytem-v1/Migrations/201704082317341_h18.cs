namespace AdminstrationSysytem_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructor_Corse_InDepartment", "InstructorEvaluation", c => c.Int(nullable: false));
            AddColumn("dbo.Instructor_Corse_InDepartment", "CourseStatues", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructor_Corse_InDepartment", "CourseStatues");
            DropColumn("dbo.Instructor_Corse_InDepartment", "InstructorEvaluation");
        }
    }
}
