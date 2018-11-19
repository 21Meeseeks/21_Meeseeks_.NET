namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.organigram")]
    public partial class organigram
    {
        [Key]
        public int idOrganigram { get; set; }

        [StringLength(255)]
        public string assignmentManager { get; set; }

        [StringLength(255)]
        public string financialManager { get; set; }

        public DateTime? organigramDate { get; set; }

        [StringLength(255)]
        public string programName { get; set; }

        [StringLength(255)]
        public string projectManagerName { get; set; }

        [StringLength(255)]
        public string projectName { get; set; }

        public int? client_idUser { get; set; }

        public virtual client client { get; set; }
    }
}
