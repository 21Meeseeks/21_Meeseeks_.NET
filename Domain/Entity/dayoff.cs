namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("map.dayoff")]
    public partial class dayoff
    {
        [Key]
        public int idLeave { get; set; }

        public DateTime? endDate { get; set; }

        public DateTime? startDate { get; set; }

        public int? leaveType_idLeaveType { get; set; }

        public int? resource_idUser { get; set; }

        public virtual resource resource { get; set; }

        public virtual leavetype leavetype { get; set; }
    }
}
