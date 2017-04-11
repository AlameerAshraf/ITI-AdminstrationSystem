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
        public DateTime BD { get; set; }
        public int degree { get; set; }
        public virtual List<Courses> Courses { get; set; }
        public int Age {
            get
            {
               // return (DateTime.Now.Year - BD.Year);
                DateTime now = DateTime.Today;
                int age = now.Year - BD.Year;
                if (now < BD.AddYears(age))
                    age--;
                return age;
            }
        }
        public string Name {
            get
            {
                return Fname + " " + Lname;
            }
        }



        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual List<Attendance> Attendance { get; set; }
    }
}