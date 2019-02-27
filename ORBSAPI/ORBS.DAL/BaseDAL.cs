using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ORBS.DAL
{
    public class BaseDAL
    {
        private readonly string _connectionStringValues;

        private IDbConnection _dbConnection;

        public BaseDAL(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService(typeof(IConfiguration)) as IConfiguration;
            if (config != null) _connectionStringValues = config["ConnectionString"];
        }

        public IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = new SqlConnection(_connectionStringValues);
                }
                if (_dbConnection.Database == "")
                {
                    _dbConnection = new SqlConnection(_connectionStringValues);
                }
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }
                return _dbConnection;
            }
        }


    }
}
