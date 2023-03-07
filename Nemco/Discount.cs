namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillId { get; set; }

        [Column("Discount")]
        public double? Discount1 { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
