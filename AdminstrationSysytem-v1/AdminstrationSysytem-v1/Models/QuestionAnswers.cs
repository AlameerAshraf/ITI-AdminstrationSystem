using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminstrationSysytem_v1.Models
{
    public class QuestionAnswers
    {
        [Column(Order = 0), Key]
        public string QuestionAnswer { get; set; }

        [Column(Order = 1), Key, ForeignKey("Questions")]
        public int QuestionId { get; set; }
        public Questions Questions { get; set; }
    }
}