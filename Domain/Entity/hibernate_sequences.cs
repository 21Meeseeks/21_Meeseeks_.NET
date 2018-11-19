namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.hibernate_sequences")]
    public partial class hibernate_sequences
    {
        [Key]
        [StringLength(255)]
        public string sequence_name { get; set; }

        public long? next_val { get; set; }
    }
}
