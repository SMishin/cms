using Microsoft.AspNet.Mvc;

namespace CMS.Controllers
{
    public class CmsController : Controller
	{
		[Route("_cms")]
		public IActionResult Index()
	    {
		    return View();
	    }
    }
}
