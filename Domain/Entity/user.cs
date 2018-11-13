namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.user")]
    public partial class user
    {
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
    }
}
