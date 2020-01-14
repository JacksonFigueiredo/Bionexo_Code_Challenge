using API.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("Capture")]
    public class Capture : BaseEntity
    {
        [Column("ComputerName")]
        public string ComputerName { get; set; }
        [Column("CaptureDate")]
        public DateTime CaptureDate { get; set; }
    }
}
