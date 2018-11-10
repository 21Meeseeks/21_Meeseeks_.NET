namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.message")]
    public partial class message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public message()
        {
            message_user = new HashSet<message_user>();
        }

        [Key]
        public int idMessage { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        public DateTime? sendingDate { get; set; }

        public int? conversation_idConversation { get; set; }

        public int? sender_idUser { get; set; }

        public virtual conversation conversation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<message_user> message_user { get; set; }
    }
}
