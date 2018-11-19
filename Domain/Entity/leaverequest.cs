namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.leaverequest")]
    public partial class leaverequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public leaverequest()
        {
            resources = new HashSet<resource>();
        }

        [Key]
        public int idLeaveRequest { get; set; }

        public DateTime? depositDate { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public int? leaveType_idLeaveType { get; set; }

        public int? resource_idUser { get; set; }

        public virtual resource resource { get; set; }

        public virtual leavetype leavetype { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<resource> resources { get; set; }
    }
}
