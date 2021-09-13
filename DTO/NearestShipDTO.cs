using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsBackEnd.DTO
{
    public class NearestShipDTO
    {
        public int ShipId { get;set; }
        public string ShipName { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
        public string PortCountry { get; set; }
        public string ArrivalTime { get; set; }
    }
}
