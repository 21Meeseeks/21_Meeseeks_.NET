namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.clientcategory")]
    public partial class clientcategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clientcategory()
        {
            clients = new HashSet<client>();
        }

        [Key]
        public int idCategory { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<client> clients { get; set; }
    }
}
