using ORBS.Model.Models;
using System.Collections.Generic;

namespace ORBS.DAL.IDataAccessLayer
{
    public interface ITableDAL
    {
        IList<Table> GetTables();

        void CreateUpdateTable();

        void Delete(int id);
    }
}
