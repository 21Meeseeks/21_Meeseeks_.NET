using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class LeaveRequest
    {
        [Key]
        public int idLeaveRequest { get; set; }
        public string description { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public DateTime? depositDate { get; set; }
        public LeaveType leaveType { get; set; }
        public bool archived { get; set; }
    }
}