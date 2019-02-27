using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ORBS.DAL.IDataAccessLayer;

namespace ORBSAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/MenuCuisine")]
    public class MenuCuisineController : Controller
    {
        private readonly IMenuCuisineDAL _cuisineDal;
        private readonly ILogger<MenuCuisineController> _logger;
        public MenuCuisineController(IMenuCuisineDAL menuCuisineDal, ILogger<MenuCuisineController> logger)
        {
            _logger = logger;
            _cuisineDal = menuCuisineDal;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult GetList()
        {
            return Ok(_cuisineDal.List());
        }


        [Route("Create")]
        public IActionResult SaveMenuCuisine()
        {
            _cuisineDal.SaveMenuCuisine();
            return Ok("successfully update");
        }
    }
}