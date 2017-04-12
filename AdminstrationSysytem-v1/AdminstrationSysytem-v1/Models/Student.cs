using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    [Table("Student")]
    public class Student : ApplicationUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Address { get; set; }
        public DateTime? BD { get; set; }
        public virtual List<Courses> Courses { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - BD.Value.Year;
                if (now < BD.Value.AddYears(age))
                    age--;
                return age;
            }
        }





        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual List<Attendance> Attendance { get; set; }
    }
}