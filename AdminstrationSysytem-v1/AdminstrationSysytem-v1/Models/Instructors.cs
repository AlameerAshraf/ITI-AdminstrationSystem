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
        public DateTime? BD { get; set; }
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

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Departments Department { get; set; }
        public virtual List<Courses> Courses { get; set; }

    }
}