using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsBackEnd.DTO
{
    public class AddShipDTO
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public string Status { get; set; }
        public float ReferenceSpeed { get; set; }
        public string Location { get; set; }
        public decimal Latitude  { get; set; }
        public decimal Longitude { get; set; }
    }
}
