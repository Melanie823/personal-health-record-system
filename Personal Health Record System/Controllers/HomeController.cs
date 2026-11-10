using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Health_Record_System.Data;
using Personal_Health_Record_System.Models;
using System.Diagnostics;

namespace Personal_Health_Record_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Personal_Health_Record_SystemContext _context;

        public HomeController(ILogger<HomeController> logger, Personal_Health_Record_SystemContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string account, string password)
        {
            var user = await _context.personal
                                    .FirstOrDefaultAsync(p => p.Account == account);

            if (user == null)
            {
                ViewBag.ErrorMessage = "無此帳號，請註冊。";
                return View();
            }

            if (user.Password != password)
            {
                ViewBag.ErrorMessage = "密碼錯誤。";
                return View();
            }

            // 登入成功，儲存帳號於 Session
            HttpContext.Session.SetString("UserAccount", user.Account);

            // 登入成功後導向Login.cshtml
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
