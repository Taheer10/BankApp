using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace bankapp.Models
{
    public class Municipality
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string MunicipalityName { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}
