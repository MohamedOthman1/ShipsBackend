using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ShipsBackEnd.DTO;
using ShipsBackEnd.Models;
using ShipsBackEnd.Utils;

namespace ShipsBackEnd.Repositories
{
    public class PortsRepo : IPortsRepo
    {
        private readonly ILogHelper _logHelper;

        public PortsRepo (ILogHelper logHelper) {
            _logHelper = logHelper;
        }
        public  IList<Port> GetAll () 
        {
            try
            {
                Port port = new Port();
                return port.AllPorts();
            }
            catch (Exception ex)
            {
                _logHelper.ErrorLogger("Failed to retrieve all ships", ex);
                return null;
            }
        }

        public  NearestShipDTO GetNearestPort(Ship ship, IList<Port> ports) 
        {
            try
            {
                double nearestShipValue = double.MaxValue;
                NearestShipDTO nearestShip = new NearestShipDTO();
                nearestShip.ShipId = ship.Id;
                nearestShip.ShipName = ship.Name;
                foreach (Port port in ports)
                {
                    var shipLat = port.Latitude * (Math.PI / 180.0);
                    var shipLong = port.Longitude * (Math.PI / 180.0);
                    var portLat = ship.Latitude * (Math.PI / 180.0);
                    var portLong = ship.Longitude * (Math.PI / 180.0) - shipLong;
                    var dist = Math.Pow(Math.Sin((portLat - shipLat) / 2.0), 2.0) +
                             Math.Cos(shipLat) * Math.Cos(portLat) * Math.Pow(Math.Sin(portLong / 2.0), 2.0);
                   var distance = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(dist), Math.Sqrt(1.0 - dist)));
                   if (distance < nearestShipValue)
                   {
                       nearestShip.PortCountry = port.Country;
                       nearestShip.PortId = port.Id;
                       nearestShip.PortName = port.Name;
                       nearestShipValue = distance; // value in meters
                   }
                }

                nearestShip.ArrivalTime = Convert.ToString((nearestShipValue * 0.000621371192) / ship.ReferenceSpeed,
                    CultureInfo.InvariantCulture); // in hours;
                return nearestShip;
            }
            catch (Exception ex)
            {
                _logHelper.ErrorLogger("Failed to get nearest port", ex);
                return null;
            }
        }

        public Port GetPort(int id)
        {
            try
            {
                Port port = new Port();
                var selectedPort = port.AllPorts().Find(x => x.Id == id);
                return selectedPort;
            }
            catch (Exception ex)
            {
                _logHelper.ErrorLogger("Failed to get port " + id, ex);
                return null;
            }
        }
    }
}
