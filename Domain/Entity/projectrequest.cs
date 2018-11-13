namespace Domain.Entity
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
            admins = new HashSet<admin>();
            competences = new HashSet<competence>();
        }

        [Key]
        public int idRequest { get; set; }

        [Column(TypeName = "bit")]
        public bool archived { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateEndProject { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateStartProject { get; set; }

        public DateTime? depositDate { get; set; }

        [StringLength(255)]
        public string descriptionProject { get; set; }

        [StringLength(255)]
        public string nameProject { get; set; }

        [StringLength(255)]
        public string presentedBy { get; set; }

        public int? requestStatus { get; set; }

        public int resourcesNumber { get; set; }

        public DateTime? reviewDate { get; set; }

        public int? client_idUser { get; set; }

        public virtual client client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<admin> admins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<competence> competences { get; set; }
    }
}
