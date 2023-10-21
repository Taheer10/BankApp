using System.ComponentModel.DataAnnotations;

namespace bankapp.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string DistrictName { get; set; }
    }
}
