using System.Collections.Generic;
using System.Threading.Tasks;
using ShipsBackEnd.DTO;
using ShipsBackEnd.Models;

namespace ShipsBackEnd.Repositories
{
    public interface IPortsRepo
    {
        IList<Port> GetAll();
        NearestShipDTO GetNearestPort(Ship ship, IList<Port> ports);

        Port GetPort(int id);

    }
}