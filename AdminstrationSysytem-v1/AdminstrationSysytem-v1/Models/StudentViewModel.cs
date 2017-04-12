using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class StudentViewModel : RegisterViewModel
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Address { get; set; }

    }
}