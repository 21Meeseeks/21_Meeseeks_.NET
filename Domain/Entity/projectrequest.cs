namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.projectrequest")]
    public partial class projectrequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public projectrequest()
        {
            projectrequest_competence = new HashSet<projectrequest_competence>();
            projectrequest_competence1 = new HashSet<projectrequest_competence>();
            admins = new HashSet<admin>();
        }

        [Key]
        public int idRequest { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateEndProject { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateStartProject { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime depositDate { get; set; }

        [StringLength(255)]
        public string descriptionProject { get; set; }

        [StringLength(255)]
        public string nameProject { get; set; }

        public int resourcesNumber { get; set; }

        public int? client_idUser { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        [StringLength(255)]
        public string presentedBy { get; set; }

        [Column(TypeName = "bit")]
        public bool archived { get; set; }

        public int? requestStatus { get; set; }

        public DateTime? reviewDate { get; set; }

        public virtual client client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<projectrequest_competence> projectrequest_competence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<projectrequest_competence> projectrequest_competence1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin> admins { get; set; }
    }
}
