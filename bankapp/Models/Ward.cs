
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace bankapp.Models
{
    public class Ward
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(75)]
        public int WardNo { get; set; }

        [ForeignKey("Municipality")]
        public int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }
    }
}
