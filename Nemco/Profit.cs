namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profit")]
    public partial class Profit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(30)]
        public string DateTime { get; set; }

        [Column("Profit")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? Profit1 { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
