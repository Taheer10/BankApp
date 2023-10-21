using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankapp.Models
{
    public class Tole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string ToleName { get; set; }
        [ForeignKey("Ward")]
        public int WardId { get; set; }
        public virtual Ward Ward { get; set; }
    }
}
