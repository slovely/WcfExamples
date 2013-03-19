using System.Web.Mvc;
using WcfExamples.Contracts;

namespace WcfExamples.Web.Controllers
{
    public class WcfController : Controller
    {
        private readonly IExampleService _service;
        private readonly IDatabaseService _databaseService;

        public WcfController(IExampleService service, IDatabaseService databaseService)
        {
            _service = service;
            _databaseService = databaseService;
        }

        [HttpPost]
        public JsonResult GetDataFromWcfService()
        {
            var result = _service.MethodThatReturnsComplexType();
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetDataFromDapper(int id)
        {
            var result = _databaseService.GetObjectFromDatabase(id);
            return Json(result);
        }
    }
}