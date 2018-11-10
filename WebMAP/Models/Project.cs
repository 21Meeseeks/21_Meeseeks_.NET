using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class Project
    {
        public int idProject { get; set; }
        public DateTime? dateEnd { get; set; }
        public DateTime? dateStart { get; set; }

        public string name { get; set; }

        public string picture { get; set; }

        public int? projectType { get; set; }

        public int? client_idUser { get; set; }

        public string description { get; set; }

    }
}