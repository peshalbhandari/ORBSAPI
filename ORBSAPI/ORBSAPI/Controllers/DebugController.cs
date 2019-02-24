using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ORBSAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Debug")]
    public class DebugController : Controller
    {

        [HttpGet]
        public JsonResult Get()
        {
            return Json(new
            {
                projectName = Assembly.GetExecutingAssembly().FullName,
                CurrentDate = System.DateTime.Now,
                PoweredBy = "Perfect Active Solutions"
            });
        }
    }
}