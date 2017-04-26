using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminstrationSysytem_v1.Models
{
    public class Attendance
    {
        [Column(Order = 0), Key, ForeignKey("Student")]
        public string StudentId { get; set; }
        public Student Student { get; set; }

        [Column(Order = 1), Key]
        public DateTime Date { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public DateTime? LeavingTime { get; set; }
        public bool IsPermitted { get; set; }
        

    }

    [NotMapped]
    public class AttendanceModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool IsPermitted { get; set; }
        public int? NoOfPermissions { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public int? GradeOfAbsence { get; set; }

    }
}