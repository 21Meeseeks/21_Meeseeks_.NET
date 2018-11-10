namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.projectrequest_competence")]
    public partial class projectrequest_competence
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int projectRequests_idRequest { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int competences_idCompetence { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectRequest_idRequest { get; set; }

        public virtual competence competence { get; set; }

        public virtual projectrequest projectrequest { get; set; }

        public virtual projectrequest projectrequest1 { get; set; }
    }
}
