using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminstrationSysytem_v1.Models
{
    public class Course_Student_Instructor
    {
        public int InstructorEvaluation { get; set; }
        public int StudentLabDegree { get; set; }

        [Column("InstructorId", Order = 0), Key, ForeignKey("Instructor")]
        public string InstructorId { get; set; }
        public Instructors Instructor { get; set; }

        [Column("CoursesId", Order = 1), Key, ForeignKey("Course")]
        public int CoursId { get; set; }
        public Courses Course { get; set; }


        [Column("StudentId", Order = 2), Key, ForeignKey("Student")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}