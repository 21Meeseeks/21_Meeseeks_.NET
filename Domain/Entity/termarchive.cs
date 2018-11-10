namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.termarchive")]
    public partial class termarchive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idTermArchive { get; set; }

        public int? term_idProject { get; set; }

        public int? term_idResource { get; set; }

        public int? term_idTerm { get; set; }

        public virtual term term { get; set; }
    }
}
