using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    [Table("Instructors")]
    public class Instructors : ApplicationUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string GraduationYear { get; set; }
        public string Status { get; set; }

        public DateTime BD { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - BD.Year;
                if (now < BD.AddYears(age))
                    age--;
                return age;
            }
        }




        public Departments Department { get; set; }

        public virtual List<Courses> Courses { get; set; }

    }
}