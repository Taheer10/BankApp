using bankapp.Models.member;
using bankapp.Models.NewAccount;

namespace bankapp.Models.Loan
{
    public class Loan
    {
        public int Id { get; set; }
        public string LoanNo { get; set; }
        public int LoanRate { get; set; }
        public int LoanAmt { get; set; }
        public int LoanDuration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int LoanTypeId { get; set; }
        public virtual LoanType LoanType { get; set; }
    }
}
