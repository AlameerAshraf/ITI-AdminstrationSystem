using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{

    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }






        public virtual List<Student> Students { get; set; }



        [ForeignKey("Instructor")]
        [Column("ManagerId")]
        public string InstructorId { get; set; }
        public Instructors Instructor { get; set; }
        



        [InverseProperty("Department")]
        public virtual List<Instructors> Instructors { get; set; }

    }
}