using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShipsBackEnd.Models;

namespace ShipsBackEnd.Repositories
{
    public interface IShipsRepo
    {
        Task<Ship> GetShip(int id);
        IList<Ship> GetAll();
    }
}
