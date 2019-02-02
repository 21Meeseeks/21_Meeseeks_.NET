using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class EstablishementVM
    {
        [Key]
        public int idEstablishment { get; set; }
        public string nameEstablishment { get; set; }
        [DataType(DataType.Date)]
        public DateTime? startDate { get; set; }
        [DataType(DataType.Date)] 
        public DateTime? endDate { get; set; }
        public string diplome { get; set; }
        public string description { get; set; }
    }
}