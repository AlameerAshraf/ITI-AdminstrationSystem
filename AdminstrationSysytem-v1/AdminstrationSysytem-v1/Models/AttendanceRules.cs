using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    [Table("AttendanceRules")]

    public class AttendanceRules
    {
        [Key]
        public int RuleId { get; set; }
        public int PunshimentNumber { get; set; }

        //Number of absence time !
        public int RuleCase { get; set; }
    }
}