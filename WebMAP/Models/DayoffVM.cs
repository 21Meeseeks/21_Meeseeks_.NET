using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class DayoffVM
    {
        [Key]
        public int idLeave { get; set; }

        public DateTime? endDate { get; set; }

        public DateTime? startDate { get; set; }

        public leavetypeVM leaveType { get; set; }

        public ResourceVM resource { get; set; }
    }
}