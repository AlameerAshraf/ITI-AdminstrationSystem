using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminstrationSysytem_v1.Models
{
    public class Course_Student_Exam
    {
        public int ExamGrade { get; set; }

        [Column("StudentId", Order = 0), Key, ForeignKey("Students")]
        public string StudentId { get; set; }
        public Student Students { get; set; }

        [Column("CoursesId", Order = 1), Key, ForeignKey("Course")]
        public int CoursId { get; set; }
        public Courses Course { get; set; }


        [Column("ExamId", Order = 2), Key, ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}