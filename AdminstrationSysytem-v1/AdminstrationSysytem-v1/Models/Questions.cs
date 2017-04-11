using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class Questions
    {
        [Key]
        public int QuestionId { get; set; }
        public int QuestionBody { get; set; }
        public int QuestionType { get; set; }
        public int QuestionRightAnswer { get; set; }

        [ForeignKey("Courses")]
        public int CoursId { get; set; }
        public Courses Courses { get; set; }
        public virtual List<Exam> Exams { get; set; }
    }
}