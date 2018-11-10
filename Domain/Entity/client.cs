namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.client")]
    public partial class client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public client()
        {
            projects = new HashSet<project>();
            organigrams = new HashSet<organigram>();
            projectrequests = new HashSet<projectrequest>();
            notes = new HashSet<note>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUser { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        public DateTime? lastAuthentificated { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        public DateTime? passwordLastChanged { get; set; }

        [StringLength(255)]
        public string phoneNumber { get; set; }

        [StringLength(255)]
        public string clientName { get; set; }

        [StringLength(255)]
        public string logo { get; set; }

        public int? clientCategory_idCategory { get; set; }

        public int? clientType_idClientType { get; set; }

        [StringLength(255)]
        public string clientType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project> projects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<organigram> organigrams { get; set; }

        public virtual clienttype clienttype1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<projectrequest> projectrequests { get; set; }

        public virtual clientcategory clientcategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<note> notes { get; set; }
    }
}
