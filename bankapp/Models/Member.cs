using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace bankapp.Models.member
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not Valid")]
        public string EmailId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Contact No is not Valid")]
        public int ContactNo { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [ForeignKey("District")]
        [DisplayName("District")]
        public int DistrictId { get; set; }
        public virtual District District { get; set; }



        [Required]
        [ForeignKey("Municipality")]
        [DisplayName("Municipality")]
        public int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }


        [Required]
        [ForeignKey("Ward")]
        [DisplayName("Ward")]
        public int WardId { get; set; }
        public virtual Ward Ward { get; set; }

        [Required]
        [ForeignKey("Tole")]
        [DisplayName("Tole")]
        public int ToleId { get; set; }
        public virtual Tole Tole { get;set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
