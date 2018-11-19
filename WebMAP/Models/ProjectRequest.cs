using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class ProjectRequest
    {
        [Key]
        public int idRequest { get; set; }
        public DateTime? dateStartProject { get; set; }
        public DateTime? dateEndProject { get; set; }
        public DateTime depositDate { get; set; }
        public string descriptionProject { get; set; }
        public string nameProject { get; set; }
        public int? resourcesNumber { get; set; }
        public string comments { get; set; }
        public string presentedBy { get; set; }
        public bool archived { get; set; }
        public DateTime? reviewDate { get; set; }
        public string requestStatus { get; set; }
        public IEnumerable<Competence> competences { get; set; }
    }

    public class ProjectRequestDBContext : DbContext
    {
        public DbSet<ProjectRequest> ProjectRequests { get; set; }
    }
}