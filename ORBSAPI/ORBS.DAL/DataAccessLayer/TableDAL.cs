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
    public class TableDAL : BaseDAL, ITableDAL
    {
        private readonly ILogger<TableDAL> _logger = null;
        public TableDAL(IServiceProvider serviceProvider, ILogger<TableDAL> logger) : base(serviceProvider)
        {
            _logger = logger;
        }
        public IList<Table> GetTables()
        {
            IList<Table> tableList;
            _logger.LogInformation($"logger started for TableDAL:GetTables");
            try
            {
                using (IDbConnection conn = Connection)
                {
                    tableList = conn.Query<Table>("dbo.UDP_GetTableRecords",
                        commandType: CommandType.StoredProcedure
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured in getting data from the database for table records");
                _logger.LogError(ex.Message);
                throw;
            }
            _logger.LogInformation($" getting table data from the database completed.");
            return tableList;
        }

        public void CreateUpdateTable()
        {
            //dummy data to save to database
            var tableItemToSave = new
            {
                @p_id = 3,
                @p_name = "Table-Updated-1",
                @p_description = "Updated Table from API",
                @p_code = "TU1",
                @p_IsActive = 0
            };
            _logger.LogInformation($"logger started for TableDAL:CreateUpdateTable");
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var result = conn.Execute("dbo.UDP_CreateUpdateTableRecords", tableItemToSave,
                          commandType: CommandType.StoredProcedure
                      );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured in getting data from the database for table records");
                _logger.LogError(ex.Message);
                throw;
            }
            _logger.LogInformation($" getting table data from the database completed.");


        }

        public void Delete(int id)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var query = conn.Execute("dbo.UDP_DeleteTable", new { @p_id = id }
                    , commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while deleting data with id:{id} from table in database");
                _logger.LogError(ex.Message);
                throw;
            }
            _logger.LogInformation($" deleting table data with id {id} from the database completed.");
        }
    }
}
