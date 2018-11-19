using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class Resource
    {
        public int idUser { get; set; }

        public string address { get; set; }

        public string email { get; set; }

        public DateTime? lastAuthentificated { get; set; }

        public string password { get; set; }

        public DateTime? passwordLastChanged { get; set; }

        public string phoneNumber { get; set; }

        public string availability { get; set; }

        public string contractType { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
    }
}