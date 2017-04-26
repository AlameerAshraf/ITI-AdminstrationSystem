using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class AttendanceRules
    {
        public int RuleId { get; set; }
        public int PunshimentNumber { get; set; }

        //Number of absence time !
        public int RuleCase { get; set; }
    }
}