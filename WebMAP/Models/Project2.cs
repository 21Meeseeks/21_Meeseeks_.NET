using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class Project2
    {
        public int idProject { get; set; }
        public DateTime? dateEnd { get; set; }
        public DateTime? dateStart { get; set; }
        public string client { get; set; }
        public string name { get; set; }

        public string picture { get; set; }

        public string projectType { get; set; }


        public string description { get; set; }

    }
}