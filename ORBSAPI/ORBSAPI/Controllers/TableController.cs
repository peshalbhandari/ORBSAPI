using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ORBS.DAL.IDataAccessLayer;

namespace ORBSAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Table")]
    public class TableController : Controller
    {
        private readonly ILogger<TableController> _logger;
        private readonly ITableDAL _tableDal;

        public TableController(ITableDAL tableDal, ILogger<TableController> logger)
        {
            _logger = logger;
            _tableDal = tableDal;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetTables()
        {
            _logger.LogInformation($"Started logging for Table Controller::method:GetTables");
            return Ok(_tableDal.GetTables());
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateTable()
        {
            _logger.LogInformation($"Started logging for Table Controller::method:CreateTable");
            _tableDal.CreateUpdateTable();
            return Ok("succesfully updated");
        }

        [Route("delete")]
        public IActionResult DeleteTable([FromQuery]string id)
        {
            _logger.LogInformation($"Started logging for Table Controller::method:DeleteTable");
            _tableDal.Delete(int.Parse(id));
            return Ok("deleted updated");
        }
    }
}