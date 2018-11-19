using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class Competence
    {
        [Key]
        public int idCompetence { get; set; }
        public string label { get; set; }
        public string description { get; set; }
    }
}