using ORBS.Model.Models;
using System.Collections.Generic;

namespace ORBS.DAL.IDataAccessLayer
{
    public interface IMenuCuisineDAL
    {
        IList<MenuCuisine> List();

        bool SaveMenuCuisine();
    }
}
