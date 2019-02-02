using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class ResourceVM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUser { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        public DateTime? lastAuthentificated { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        public DateTime? passwordLastChanged { get; set; }

        [StringLength(255)]
        public string phoneNumber { get; set; }

        [StringLength(255)]
        public string availability { get; set; }

        [StringLength(255)]
        public string contractType { get; set; }

        [StringLength(255)]
        public string firstName { get; set; }

        [StringLength(255)]
        public string lastName { get; set; }

        [StringLength(255)]
        public string photo { get; set; }

        public double? rate { get; set; }

        [Column(TypeName = "bit")]
        public bool state { get; set; }

        public ResumeVM resume { get; set; }


        public SeniorityVM seniority { get; set; }
    }
}