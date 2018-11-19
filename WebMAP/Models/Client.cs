using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class Client
    {
        [Key]
        public int idUser { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string clientName { get; set; }
        public string clientType { get; set; }
        public string logo { get; set; }
        public ClientCategory clientCategory { get; set; }



    }
}