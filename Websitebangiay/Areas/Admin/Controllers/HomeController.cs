using Microsoft.AspNetCore.Mvc;

namespace Websitebangiay.Areas.Admin.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult IndexA()
		{
			return View();
		}
	}
}
