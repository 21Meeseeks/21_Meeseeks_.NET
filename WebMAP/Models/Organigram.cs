using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Entity;

namespace WebMAP.Models
{
    public class Organigram
    {   [Key]
        public int idOrganigram { get; set; }

        public string assignmentManager { get; set; }

        public string financialManager { get; set; }

        public DateTime? organigramDate { get; set; }
        public Project project { get; set; }
        public string programName { get; set; }

        public string projectManagerName { get; set; }




    }
}