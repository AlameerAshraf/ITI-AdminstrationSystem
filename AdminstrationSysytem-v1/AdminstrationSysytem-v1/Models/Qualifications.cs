using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class Qualifications
    {
        [Column(Order = 0) , Key]
        public string QualificationName { get; set; }

        [Column(Order = 1), Key , ForeignKey("Instructors")]
        public string InstructorId { get; set; }


        public virtual Instructors Instructors  { get; set; }


    }
}