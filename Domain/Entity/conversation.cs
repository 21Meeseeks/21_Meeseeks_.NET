namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.conversation")]
    public partial class conversation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conversation()
        {
            conversation_user = new HashSet<conversation_user>();
            messages = new HashSet<message>();
        }

        [Key]
        public int idConversation { get; set; }

        public int? conversationType { get; set; }

        public DateTime? startDate { get; set; }

        [StringLength(255)]
        public string subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conversation_user> conversation_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<message> messages { get; set; }
    }
}
