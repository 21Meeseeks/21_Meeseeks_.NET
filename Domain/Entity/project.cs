namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.project")]
    public partial class project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public project()
        {
            terms = new HashSet<term>();
            competences = new HashSet<competence>();
            terms1 = new HashSet<term>();
        }

        [Key]
        public int idProject { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateEnd { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateStart { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string picture { get; set; }

        [StringLength(50)]
        public string projectType { get; set; }

        public int? client_idUser { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public virtual client client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<term> terms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<competence> competences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<term> terms1 { get; set; }
    }
}
