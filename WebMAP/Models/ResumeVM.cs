using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class ResumeVM
    {
        [Key]
        public int idResume { get; set; }

        [StringLength(255)]
        public string description { get; set; }
        public List<EstablishementVM> etablissement { get; set; }
        public List<SocietyVM> society { get; set; }
        public List<CertificatVM> certificates { get; set; }
        public List<CompetenceVM> competence { get; set; }


    }
}