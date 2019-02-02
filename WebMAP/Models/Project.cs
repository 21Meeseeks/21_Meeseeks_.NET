using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Entity;

namespace WebMAP.Models
{
    public class Project
    {
        [Key]
        public int idProject { get; set; }
        public DateTime? dateEnd { get; set; }
        public DateTime? dateStart { get; set; }
        public Client client { get; set; }
        public string name { get; set; }

        public string picture { get; set; }

        public string projectType { get; set; }


        public string description { get; set; }

    }
}