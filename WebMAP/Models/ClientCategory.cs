using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class ClientCategory
    {
        [Key]
        public int idCategory { get; set; }
       
        public string name { get; set; }

        public string description { get; set; }
    }
}