using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class SocietyVM
    {
        [Key]
        public int idSociety { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? endDate { get; set; }
    }
}