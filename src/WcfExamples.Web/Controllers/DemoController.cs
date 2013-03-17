using System.Web.Mvc;

namespace WcfExamples.Web.Controllers
{
    public class DemoController : Controller
    {
        [HttpGet]
        public ActionResult DisplayView()
        {
            return View();
        }
    }
}