using Dapper;
using Microsoft.Extensions.Logging;
using ORBS.DAL.IDataAccessLayer;
using ORBS.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ORBS.DAL.DataAccessLayer
{
    public class MenuCuisineDAL : BaseDAL, IMenuCuisineDAL
    {
        private readonly ILogger<MenuCuisineDAL> _logger;
        public MenuCuisineDAL(ILogger<MenuCuisineDAL> logger, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        public IList<MenuCuisine> List()
        {
            IList<MenuCuisine> menuCuisines = null;
            _logger.LogInformation($"DAL started for menu cuisine. Method: List");
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //conn.Open();
                    menuCuisines = conn.Query<MenuCuisine>("dbo.UDP_GetMenuCuisine",
                        commandType: CommandType.StoredProcedure).ToList();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured in getting data from the database for menu cuisine");
                _logger.LogError(ex.Message);
                throw;
            }
            _logger.LogInformation($" getting menu cuisine data from the database completed.");
            return menuCuisines;

        }


        public bool SaveMenuCuisine()
        {
            _logger.LogInformation($"DAL started for menu cuisine. Method: SaveMenuCuisine");
            try
            {
                var param = new { @p_id = 123, @p_Name = "Test Cuisine", @p_Desc = "nothing", @p_DisplaySeq = 1 };
                using (IDbConnection conn = base.Connection)
                {
                    var a = conn.Execute("dbo.UDP_UpdateSaveMenuCuisine",
                        param, commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured in getting data from the database for menu cuisine");
                _logger.LogError(ex.Message);
                throw;
            }
            _logger.LogInformation($" getting menu cuisine data from the database completed.");
            return true;
        }
    }
}
