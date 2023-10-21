using bankapp.Data;
using bankapp.Models.member;
using bankapp.Models.NewAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bankapp.Controllers.NewAccount
{
    public class WithdrawController : Controller
    {
        private readonly AppDbContext _context;
        public WithdrawController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<WithdrawAccount> withdrawAccounts = _context.withdrawAccount
                .Include(a => a.AccountCreate)
                .Include(t => t.AccountType)
                .ToList();
            return View(withdrawAccounts);
        }


        private List<SelectListItem> GetAccounts()
        {
            var lstaccounts = new List<SelectListItem>();
            List<AccountCreate> accounts = _context.accountCreate.ToList();

            lstaccounts = accounts.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.AccNo,
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = " ",
                Text = "---Select AccountNo---",
            };

            lstaccounts.Insert(0, defItem);
            return lstaccounts;
        }

        private List<SelectListItem> GetAccountType()
        {
            var lstAccounts = new List<SelectListItem>();
            List<AccountType> accountTypes = _context.accountType.ToList();

            lstAccounts = accountTypes.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = (ct.AccountName).ToString()
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = " ",
                Text = "---select Account Type---",
            };

            lstAccounts.Insert(0, defItem);
            return lstAccounts;
        }


        [HttpGet]
        public IActionResult Create()
        {
 
            WithdrawAccount withdrawAccount = new WithdrawAccount();
            ViewBag.AccountCreateId = GetAccounts();
            ViewBag.AccountTypeId = GetAccountType();

            return View(withdrawAccount);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Create(WithdrawAccount withdrawAccount)
        {
            
                if (withdrawAccount.WithdrawAmt > 0)
                {
                    _context.Add(withdrawAccount);
                    _context.SaveChanges();
                    RedirectToAction(nameof(List));
                }
                else
                {
                    return Json("Withdraw Amount must be greater than 0");

                }
            return RedirectToAction(nameof(List));
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            WithdrawAccount withdrawAccount = GetWithdrawAccount(Id);
            ViewBag.AccountCreateId = GetAccountNo(withdrawAccount.AccountCreateId);
            ViewBag.AccountTypeId = GetAccountTypeName(withdrawAccount.AccountTypeId);
            return View(withdrawAccount);

        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Edit(WithdrawAccount withdrawAccount)
        {

            if (withdrawAccount.WithdrawAmt > 0)
            {
                _context.Attach(withdrawAccount);
                _context.Entry(withdrawAccount).State = EntityState.Modified;
                _context.SaveChanges();
                 RedirectToAction(nameof(List));
            }
            else
            {
                 Json("Withdraw Amount must be greater than 0");

            }
              return RedirectToAction(nameof(List));
        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            WithdrawAccount withdrawAccount = GetWithdrawAccount(Id);
            ViewBag.AccountNo = GetAccountNo(withdrawAccount.AccountCreateId);
            ViewBag.GetAccountTypeName = GetAccountTypeName(withdrawAccount.AccountTypeId);
            return View(withdrawAccount);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Delete(WithdrawAccount withdrawAccount)
        {
            _context.Attach(withdrawAccount);
            _context.Entry(withdrawAccount).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }




        private WithdrawAccount GetWithdrawAccount(int Id)
        {
            WithdrawAccount withdrawAccount = _context.withdrawAccount.Where(a => a.WId == Id).FirstOrDefault();
            return withdrawAccount;
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            WithdrawAccount withdrawAccount = GetWithdrawAccount(Id);
            ViewBag.AccountCreateId = GetAccountNo(withdrawAccount.AccountCreateId);
            ViewBag.AccountTypeId = GetAccountTypeName(withdrawAccount.AccountTypeId);


            return View(withdrawAccount);
        }


        private string GetAccountNo(int AccId)
        {
            string AccountNo = _context.accountCreate.Where(d => d.Id == AccId).SingleOrDefault().AccNo;
            return AccountNo;
        }

        private string GetAccountTypeName(int AccId)
        {
            string AccountName = _context.accountType.Where(d => d.Id == AccId).SingleOrDefault().AccountName;
            return AccountName;
        }




    }
}
