using bankapp.Data;
using bankapp.Models;
using bankapp.Models.member;
using bankapp.Models.NewAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace bankapp.Controllers
{
    public class MemberController : Controller
    {
        private readonly AppDbContext _context;

        public MemberController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Member> members = _context.members
                .Include(d => d.District)
                .Include(mu => mu.Municipality)
                .Include(w => w.Ward)
                .Include(t => t.Tole)
                .ToList();

            return View(members);
        }

        private List<SelectListItem> GetDistricts()
        {
            var lstDistricts = new List<SelectListItem>();
            List<District> districts = _context.districts.ToList();

            lstDistricts = districts.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.DistrictName
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select District----"
            };

            lstDistricts.Insert(0, defItem);

            return lstDistricts;
        }

        private List<SelectListItem> GetMunicipalities(int DistrictId=1 )
        {

            List<SelectListItem> lstMunicipalities = _context.municipality
                .Where(c => c.DistrictId == DistrictId)
                .OrderBy(n => n.MunicipalityName)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.MunicipalityName
                 }).ToList();


            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Municipality----"
            };

            lstMunicipalities.Insert(0, defItem);

            return lstMunicipalities;
        }


        private List<SelectListItem> GetWards(int MunicipalityId=1)
        {

            List<SelectListItem> lstWards = _context.ward
                .Where(c => c.MunicipalityId == MunicipalityId)
                .OrderBy(n => n.WardNo)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.WardNo.ToString()
                 }).ToList();


            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select WardNo ----"
            };

            lstWards.Insert(0, defItem);

            return lstWards;
        }

        private List<SelectListItem> GetToles(int WardId = 1)
        {

            List<SelectListItem> lstToles = _context.tole
                .Where(c => c.WardId == WardId)
                .OrderBy(n => n.ToleName)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.ToleName
                 }).ToList();


            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select ToleName ----"
            };

            lstToles.Insert(0, defItem);

            return lstToles;
        }


        [HttpGet]
        public IActionResult Create()
        {
            Member Member = new Member();
            ViewBag.DistrictId = GetDistricts();
            ViewBag.MunicipalityId = GetMunicipalities();
            ViewBag.WardId = GetWards();
            ViewBag.ToleId = GetToles();
            return View(Member);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Member member)
        {
          

            if (!ModelState.IsValid)
            {
                if (!_context.members.Any(m => m.EmailId == member.EmailId && m.ContactNo == member.ContactNo ))
                {
                    _context.Add(member);
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
        public JsonResult GetMunicipalitiesByDistrict(int districtId)
        {
            List<SelectListItem> municipalities = GetMunicipalities(districtId);
            return Json(municipalities);
        }



        [HttpGet]
        public JsonResult GetWardsByMunicipalities(int municipalityId)
        {
            List<SelectListItem> wards = GetWards(municipalityId);
            return Json(wards);
        }



        [HttpGet]
        public JsonResult GetTolesByWards(int wardId)
        {
            List<SelectListItem> toles = GetToles(wardId);
            return Json(toles);
        }

         private Member GetMember(int Id) 
        {
            Member member = _context.members
                   .Where(m => m.Id == Id).FirstOrDefault();
            return member;
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Member member = GetMember(Id);
            ViewBag.DistrictName = GetDistrictName(member.DistrictId);
            ViewBag.MunicipalityName = GetMunicipalityName(member.MunicipalityId);
            ViewBag.WardName = GetWardName(member.WardId);
            ViewBag.ToleName = GetToleName(member.ToleId);
            return View(member);
        }





        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Member member = GetMember(Id);
            ViewBag.DistrictId = GetDistricts();
            ViewBag.MunicipalityId = GetMunicipalities(member.MunicipalityId);
            ViewBag.WardId = GetWards(member.WardId);
            ViewBag.ToleId = GetToles(member.ToleId);
            return View(member);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Edit(Member member)
        {
            _context.Attach(member);
            _context.Entry(member).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Member member = GetMember(Id);
            ViewBag.DistrictName = GetDistrictName(member.DistrictId);
            ViewBag.MunicipalityName = GetMunicipalityName(member.MunicipalityId);
            ViewBag.WardName = GetWardName(member.WardId);
            ViewBag.ToleName = GetToleName(member.ToleId);
            return View(member);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Delete(Member member)
        {
            _context.Attach(member);
            _context.Entry(member).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }


        private string GetDistrictName(int DistrictId)
        {
            string DistrictName = _context.districts.Where(d => d.Id == DistrictId).SingleOrDefault().DistrictName;
            return DistrictName;
        }

        private string GetMunicipalityName(int MunicipalityId)
        {
            string MunicipalityName = _context.municipality.Where(d => d.Id == MunicipalityId).SingleOrDefault().MunicipalityName;
            return MunicipalityName;
        }

        private string GetWardName(int WardId)
        {
            string WardName = _context.ward.Where(d => d.Id == WardId).SingleOrDefault().WardNo.ToString();
            return WardName;
        }

        private string GetToleName(int ToleId)
        {
            string ToleName = _context.tole.Where(d => d.Id == ToleId).SingleOrDefault().ToleName;
            return ToleName;
        }





    }
}
