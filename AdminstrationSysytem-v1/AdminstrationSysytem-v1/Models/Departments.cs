using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}