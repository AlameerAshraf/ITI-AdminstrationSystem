using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public int StartsFrom { get; set; }
        public int EndsIn { get; set; }

        public int ExamDuration
        {
            get
            {
                return StartsFrom - EndsIn; 
            }
        }

        public int Subject { get; set; }
    }
}