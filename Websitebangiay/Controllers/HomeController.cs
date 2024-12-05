using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nhom8_WebSiteBanGiay.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Nhom8_WebSiteBanGiay.Controllers
{
	public class HomeController : Controller
    {

        DoAnWebContext objDoAnWebContext = new DoAnWebContext();

		private readonly ILogger<HomeController> _logger;

		public object Session { get; private set; }

		public IActionResult Priacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeModels ojbHomeModels = new HomeModels();

			ojbHomeModels.ListTbMenu = objDoAnWebContext.TbMenus.ToList();

			ojbHomeModels.ListTbProduct = objDoAnWebContext.TbProducts.ToList();

            return View(ojbHomeModels) ;
		}
        [HttpGet]
		public IActionResult Register()
		{
            return View();
		}
		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(TbAccount _tbAccount)
		{
			if (ModelState.IsValid)
			{
				var check = objDoAnWebContext.TbAccounts.FirstOrDefault(s => s.Email == _tbAccount.Email);
				if (check == null)
				{
					_tbAccount.Password = GetMD5(_tbAccount.Password);
					objDoAnWebContext.Add(_tbAccount);
					objDoAnWebContext.SaveChanges();
					return RedirectToAction("Login");
				}
				else
				{
					ViewBag.error = "Email already exists";
					return View();
				}

			}
			return View();
		}
		//create a string MD5
		public static string GetMD5(string str)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] fromData = Encoding.UTF8.GetBytes(str);
			byte[] targetData = md5.ComputeHash(fromData);
			string byte2String = null;

			for (int i = 0; i < targetData.Length; i++)
			{
				byte2String += targetData[i].ToString("x2");

			}
			return byte2String;
		}
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string email, string password)
		{
			if (ModelState.IsValid)
			{


				var f_password = GetMD5(password);
				var data = objDoAnWebContext.TbAccounts.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
				if (data.Count() > 0)
				{
                    HttpContext.Session.SetString("Username", data.FirstOrDefault().Username);
                    HttpContext.Session.SetString("Email", data.FirstOrDefault().Email);
                    HttpContext.Session.SetInt32("RoleId", (int)data.FirstOrDefault().RoleId);

                    return RedirectToAction("Index");
				}
				else
				{
					ViewBag.error = "Login failed";
					return RedirectToAction("Login");
				}
			}
			return View();
		}


		//Logout
		public ActionResult Logout()
		{
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Login");
		}
    }
}

