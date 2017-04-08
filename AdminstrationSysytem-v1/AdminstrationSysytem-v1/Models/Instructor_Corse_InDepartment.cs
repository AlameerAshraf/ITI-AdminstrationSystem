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
        [Key]
        public int Id { get; set; }
        public int InstructorEvaluation { get; set; }
        public int CourseStatues { get; set; }

        [ForeignKey("Instructor")]
        [Column("InstructorId")]
        public string InstructorId { get; set; }
        public Instructors Instructor { get; set; }


        [ForeignKey("Course")]
        [Column("CoursesId")]
        public int CoursId { get; set; }
        public Courses Course { get; set; }

        [ForeignKey("Department")]
        [Column("DepartmentId")]
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }
    }
}