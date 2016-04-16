using Microsoft.AspNet.Mvc;

namespace Modules.CMS.Web.Controllers
{
    public class CmsController : Controller
    {
	    public IActionResult Index()
	    {
		    return View();
	    }
    }
}
