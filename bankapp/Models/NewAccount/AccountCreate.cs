using bankapp.Models.member;

namespace bankapp.Models.NewAccount
{
    public class AccountCreate
    {
        public int Id { get; set; }
        public string AccNo { get; set; }
        public int InterestRate { get; set; }
        public int DepositAmt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }


    }
}
