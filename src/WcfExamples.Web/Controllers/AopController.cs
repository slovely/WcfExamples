using System.Web.Mvc;
using WcfExamples.Web.Code;

namespace WcfExamples.Web.Controllers
{
    public class AopController : Controller
    {
        private IAopExampleService _aopService;

        public AopController(IAopExampleService aopService)
        {
            _aopService = aopService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public string GetTime()
        {
            return  _aopService.GetCurrentTime().ToLongTimeString();
        }
    }
}