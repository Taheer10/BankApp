using Microsoft.AspNetCore.Mvc;
using bankapp.Models.Login;
using bankapp.Data;

namespace bankapp.Controllers
{
    
    public class LoginController : Controller
    {

        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Verify(Login login)
        //{
        //    var user = _context.login.FirstOrDefault(u => u.username == login.username && u.password == login.password);

        //    if (user != null)
        //    {
        //        // Successful login
        //        return RedirectToAction("Index","Home");
        //    }
        //    else
        //    {
        //        // Invalid credentials
        //        return Json("Invalid Credentials");
        //    }
        //}

        [HttpPost]
        public IActionResult Verify(Login login)
        {
            var user = _context.login.FirstOrDefault(u => u.username == login.username && u.password == login.password);

            if (user != null)
            {
                // Successful login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Invalid credentials
                return Json("Invalid Credentials");
            }
        }




    }
}
