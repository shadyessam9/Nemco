namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeptId { get; set; }

        public string DeptName { get; set; }
    }
}
