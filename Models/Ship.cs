using System.ComponentModel.DataAnnotations;

namespace ShipsBackEnd.Models
{
    public class Ship
    { [Required]
       public int Id { get; set; }
       public string Name { get; set; }
       public int Length { get; set; }
       public int Width { get; set; }
       public string Status { get; set; }
       public float ReferenceSpeed { get; set; }
       public string Location { get; set; }
       public double Latitude   { get; set; }
       public double Longitude    { get; set; }

    }
}