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

        [HttpGet]
        public PartialViewResult GetPersonDisplayHtml()
        {
            return PartialView("Person");
        }

        [HttpPost]
        public JsonResult GetPersonData()
        {
            return Json(new Person {Name = "Joe Bloggs", Age = 42});
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}