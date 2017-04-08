using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class Courses
    {
        [Key]
        public int CoursId { get; set; }
        public string Name { get; set; }
        public int LectureDuration { get; set; }
        public int LabDuration { get; set; }
        public int TotalGrade { get; set; }

        public virtual List<Instructors> Instructors { get; set; }
        public virtual List<Departments> Departments { get; set; }

    }
}