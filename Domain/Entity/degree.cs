namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.degree")]
    public partial class degree
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idDegree { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idEstablishment { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idResume { get; set; }

        [Column(TypeName = "date")]
        public DateTime? gradYear { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public virtual establishment establishment { get; set; }

        public virtual resume resume { get; set; }
    }
}
