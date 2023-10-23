using bankapp.Data;
using bankapp.Models.member;
using bankapp.Models.NewAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace bankapp.Controllers.NewAccount
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult List()
        {
          List<AccountCreate> accountCreates = _context.accountCreate
                .Include(m => m.Member)
                .Include(a => a.AccountType)
                .ToList();
            return View(accountCreates);
        }

        private List<SelectListItem> GetMembers()
        {
            var lstMembers = new List<SelectListItem>();
            List<Member> members = _context.members.ToList();

            lstMembers = members.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.FirstName +" "+ ct.LastName,
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = " ",
                Text = "---select member---",
            };

            lstMembers.Insert(0, defItem);
            return lstMembers;
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
            AccountCreate accountCreate = new AccountCreate();
            ViewBag.MemberId = GetMembers();
            ViewBag.AccountTypeId = GetAccountType();
            
            return View(accountCreate);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Create(AccountCreate accountCreate)
        {
            if (ModelState.IsValid)
            {
                if(!_context.accountCreate.Any(m => m.AccNo == accountCreate.AccNo  && m.AccountTypeId == accountCreate.AccountTypeId  &&  m.MemberId == accountCreate.MemberId))
                {
                    _context.Add(accountCreate);
                    _context.SaveChanges();
                    RedirectToAction(nameof(List));
                }
                else
                {
                    return Json("Account already exists.");

                }


            }

            return RedirectToAction(nameof(List));
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            AccountCreate accountCreate = GetAccount(Id);
            ViewBag.MemberId = GetMemberName(accountCreate.MemberId);
            ViewBag.AccountTypeId = GetAccountName(accountCreate.AccountTypeId);
            return View(accountCreate);

        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Edit(AccountCreate accountCreate)
        {
            _context.Attach(accountCreate);
            _context.Entry(accountCreate).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            AccountCreate accountCreate = GetAccount(Id);
            ViewBag.MemberName = GetMemberName(accountCreate.MemberId);
            ViewBag.AccountType = GetAccountName(accountCreate.AccountTypeId);
            return View(accountCreate);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Delete(AccountCreate accountCreate)
        {
            _context.Attach(accountCreate);
            _context.Entry(accountCreate).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }


        private AccountCreate GetAccount(int Id)
        {
            AccountCreate accountCreate = _context.accountCreate.Where(a => a.Id == Id).FirstOrDefault();
            return accountCreate;
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            AccountCreate accountCreate = GetAccount(Id);
            ViewBag.MemberId = GetMemberName(accountCreate.MemberId);
            ViewBag.AccountTypeId = GetAccountName(accountCreate.AccountTypeId);
          

            return View(accountCreate);
        }


        private string GetMemberName(int MemId)
        {
            string MemberName = _context.members.Where(d => d.Id == MemId).SingleOrDefault().FirstName;
            return MemberName;
        }

        private string GetAccountName(int MemId)
        {
            string AccountName = _context.accountType.Where(d => d.Id == MemId).SingleOrDefault().AccountName;
            return AccountName;
        }


        //[HttpGet]
        //public JsonResult GetById(int Id)
        //{
        //    var result = _context.accountCreate.Where(x => x.Id == Id).FirstOrDefault().DepositAmt;
        //    return Json(new { data = JsonConvert.SerializeObject(result) });
        //}

        [HttpGet]
        public JsonResult GetById(int Id)
        {
            var result = _context.accountCreate
                             .Where(x => x.Id == Id)
                             .Select(x => x.DepositAmt)
                             .FirstOrDefault();
            return Json(new { data = result });
        }





    }
}
