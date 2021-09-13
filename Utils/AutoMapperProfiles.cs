using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShipsBackEnd.DTO;
using ShipsBackEnd.Models;

namespace ShipsBackEnd.Utils
{
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles () {

            CreateMap<AddShipDTO, Ship> ();

        }
    }
}
