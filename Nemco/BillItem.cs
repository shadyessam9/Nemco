namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BillItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillId { get; set; }

        public int? ItemId { get; set; }

        public int? ItemQuan { get; set; }

        public double? ItemTprice { get; set; }

        public double? ItemTprofit { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Item Item { get; set; }
    }
}
