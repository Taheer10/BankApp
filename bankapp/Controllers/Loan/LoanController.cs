using bankapp.Data;
using bankapp.Models.Loan;
using bankapp.Models.member;
using bankapp.Models.NewAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bankapp.Controllers
{
    public class LoanController : Controller
    {
        private readonly AppDbContext _context;

        public LoanController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult List()
        {
           List<Loan> loan = _context.loan
                .Include(m => m.Member)
                .Include(l => l.LoanType)
                .ToList();
            return View(loan);
        }

        private List<SelectListItem> GetMembers()
        {
            var lstMembers = new List<SelectListItem>();
            List<Member> members = _context.members.ToList();

            lstMembers = members.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.FirstName + " " + ct.LastName,
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = " ",
                Text = "---select member---",
            };

            lstMembers.Insert(0, defItem);
            return lstMembers;
        }

        private List<SelectListItem> GetLoanType()
        {
            var lstLoans = new List<SelectListItem>();
            List<LoanType> loanTypes = _context.loanType.ToList();

            lstLoans = loanTypes.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = (ct.LoanName).ToString()
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = " ",
                Text = "---select Account Type---",
            };

            lstLoans.Insert(0, defItem);
            return lstLoans;
        }

        [HttpGet]
        public IActionResult Create()
        {
            Loan loan = new Loan();
            ViewBag.MemberId = GetMembers();
            ViewBag.LoanTypeId = GetLoanType();

            return View(loan);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Create(Loan loan)
        {
            //if (ModelState.IsValid)
            //{
            //    if (!_context.loan.Any(m => m.LoanNo == loan.LoanNo && m.LoanType == loan.LoanType && m.MemberId == loan.MemberId))
            //    {
            //        _context.Add(loan);
            //        _context.SaveChanges();
            //        RedirectToAction(nameof(List));
            //    }
            //    else
            //    {
            //        return Json("Loan already exists.");

            //    }


            //}
            if (!ModelState.IsValid)
            {
                _context.Add(loan);
                _context.SaveChanges();
                RedirectToAction(nameof(List));
            }
         


            return RedirectToAction(nameof(List));
        }








    }
}
