using System.ComponentModel.DataAnnotations;

namespace bankapp.Models.NewAccount
{
    public class WithdrawAccount
    {
        [Key]
        public int WId { get; set; }
        public int WithdrawAmt { get; set; }
        public DateTime WithdrawAt { get; set; } = DateTime.Now;
        public int RemainingAmt { get; set; }
        public int AccountCreateId { get; set; }
        public virtual AccountCreate AccountCreate { get; set; }
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
    }
}
