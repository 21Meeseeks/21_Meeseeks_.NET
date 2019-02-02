using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class CertificatVM
    {
        [Key]
        public int idCertificate { get; set; }

        [StringLength(255)]
        public string descriptionCertificate { get; set; }
    }
}