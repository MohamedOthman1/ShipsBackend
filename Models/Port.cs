using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShipsBackEnd.Models
{
    public class Port
    {
        public Port(int id, string country, string name, double latitude, double longitude)
        {
            Id = id;
            Country = country;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Port()
        {

        }

        [Required]
        public int Id { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<Port> AllPorts()
        {
            List<Port> ports = new List<Port>
            {
                new Port(1, "USA", "Ship 1", 54.366666666666667, 24.466666666666665),
                new Port(2, "UK", "Ship 2", 44.366666666666667, 14.466666666666665),
                new Port(3, "France", "Ship 3", 24.366666666666667, 11.466666666666665),
                new Port(4, "Germany", "Ship 4", 14.366666666666667, 10.466666666666665)
            };
            return ports;
        }
    }

    
}
