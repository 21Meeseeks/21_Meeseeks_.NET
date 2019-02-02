using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class LeaveType
    {
        [Key]
        public int idLeaveType { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}