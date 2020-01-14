using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace API.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int? Id { get; set; }
    }
}
