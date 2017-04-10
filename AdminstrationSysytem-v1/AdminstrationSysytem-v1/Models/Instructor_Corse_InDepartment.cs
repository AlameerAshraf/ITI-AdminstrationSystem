using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class Instructor_Corse_InDepartment
    {
        public int InstructorEvaluation { get; set; }
        public int CourseStatues { get; set; }

        [Column("InstructorId", Order = 0), Key , ForeignKey("Instructor")]
        public string InstructorId { get; set; }
        public Instructors Instructor { get; set; }

        [Column("CoursesId", Order = 1), Key, ForeignKey("Course")]
        public int CoursId { get; set; }
        public Courses Course { get; set; }


        [Column("DepartmentId", Order = 2), Key, ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }
    }
}