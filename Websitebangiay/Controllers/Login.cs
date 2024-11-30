using Microsoft.AspNetCore.Mvc;

namespace Websitebangiay.Controllers
{
	public class Login : Controller
	{
		public IActionResult login()
		{
			return View();
		}
	}
}
