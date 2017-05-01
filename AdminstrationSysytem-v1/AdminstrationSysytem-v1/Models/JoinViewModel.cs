using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class JoinViewModel : RegisterViewModel
    {
     //   public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public AccessType UserType { get; set; }
        public enum AccessType
        {
            Student, Instructor
        }

    }

    
}