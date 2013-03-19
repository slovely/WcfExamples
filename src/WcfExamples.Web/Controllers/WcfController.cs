using System.Web.Mvc;
using WcfExamples.Contracts;

namespace WcfExamples.Web.Controllers
{
    public class WcfController : Controller
    {
        private readonly IExampleService _service;

        public WcfController(IExampleService service, IRequestResponseService service1)
        {
            _service = service;
        }

        [HttpPost]
        public JsonResult GetDataFromWcfService()
        {
            var result = _service.MethodThatReturnsComplexType();
            return Json(result);
        }
    }
}