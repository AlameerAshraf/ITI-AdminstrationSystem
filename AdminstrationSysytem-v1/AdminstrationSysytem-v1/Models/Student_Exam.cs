using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminstrationSysytem_v1.Models
{
    public class Student_Exam
    {
        public int ExamGrade { get; set; }

        [Column("StudentId", Order = 0), Key, ForeignKey("Students")]
        public string StudentId { get; set; }
        public Student Students { get; set; }

        [Column("ExamId", Order = 1), Key, ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

    }
}