using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ShipsBackEnd.DTO;
using ShipsBackEnd.Models;
using ShipsBackEnd.Repositories;
using ShipsBackEnd.Utils;

namespace ShipsBackEnd.Controllers
{
    [ApiController]
    public class ShipsController : ControllerBase
    {
 
        private readonly IShipsRepo _shipsRepo;
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;

        public ShipsController (IShipsRepo shipsRepo, IBaseRepo baseRepo, IMapper mapper) {
            _shipsRepo = shipsRepo;
            _baseRepo = baseRepo;
            _mapper = mapper;
        }
        [HttpGet("ships/getAll")]
        public ActionResult<IEnumerable<Ship>> GetAll()
        {
            ApiResponse apiResponse = new ApiResponse();
            var ships = _shipsRepo.GetAll();
            if (ships != null)
            {
                apiResponse.Flag = Flag.Pass;
                apiResponse.Message = "All Ships data successfully returned";
                apiResponse.Result = ships;
                apiResponse.Code = (int)HttpStatusCode.OK;
                return StatusCode(apiResponse.Code, apiResponse);
            };
            apiResponse.Flag = Flag.Fail;
            apiResponse.Message = "Failed to retrieve all ships";
            apiResponse.Result = null;
            apiResponse.Code = (int)HttpStatusCode.InternalServerError;
            return StatusCode(apiResponse.Code, apiResponse);
        }

        [HttpGet("ships/getShip/{id}")]
        public async Task<ActionResult<IEnumerable<Ship>>> GetShip(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            var ship = await _shipsRepo.GetShip(id);
            if (ship != null)
            {
                apiResponse.Flag = Flag.Pass;
                apiResponse.Message = "Retrieved ship successfully";
                apiResponse.Result = ship;
                apiResponse.Code = (int)HttpStatusCode.OK;
                return StatusCode(apiResponse.Code, apiResponse);
            }

            apiResponse.Flag = Flag.Fail;
            apiResponse.Message = "The requested ship "+id+" does not exist";
            apiResponse.Result = null;
            apiResponse.Code = (int)HttpStatusCode.InternalServerError;
            return StatusCode(apiResponse.Code, apiResponse);
        }


        [HttpPost("ships/addShip")]
        public async Task<ActionResult<IEnumerable<Ship>>> AddShip([FromBody] AddShipDTO addShip)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (addShip != null)
            {

                var ship = _mapper.Map<Ship> (addShip);

                _baseRepo.Add (ship);
                if (await _baseRepo.SaveAll())
                {
                    apiResponse.Flag = Flag.Pass;
                    apiResponse.Message = "Ship added successfully ";
                    apiResponse.Result = null;
                    apiResponse.Code = (int)HttpStatusCode.OK;
                    return StatusCode(apiResponse.Code, apiResponse);
                }
                apiResponse.Flag = Flag.Fail;
                apiResponse.Message = "Failed to save data";
                apiResponse.Result = ship;
                apiResponse.Code = (int)HttpStatusCode.InternalServerError;
                return StatusCode(apiResponse.Code, apiResponse);
            }
            apiResponse.Flag = Flag.Fail;
            apiResponse.Message = "No data to add";
            apiResponse.Result = null;
            apiResponse.Code = (int)HttpStatusCode.InternalServerError;
            return StatusCode(apiResponse.Code, apiResponse);

        }

        [HttpPut("ships/updateVelocity/{id}/{velocity}")]
        public async Task<ActionResult<IEnumerable<Ship>>> UpdateVelocity(int id, float velocity)
        {
            ApiResponse apiResponse = new ApiResponse();
            Ship ship = await _shipsRepo.GetShip(id);
            if (ship != null)
            {

                ship.ReferenceSpeed = velocity;
                if (await _baseRepo.SaveAll())
                {
                    apiResponse.Flag = Flag.Pass;
                    apiResponse.Message = "Ship updated successfully ";
                    apiResponse.Result = ship;
                    apiResponse.Code = (int)HttpStatusCode.OK;
                    return StatusCode(apiResponse.Code, apiResponse);
                }
                apiResponse.Flag = Flag.Fail;
                apiResponse.Message = "Failed to update ship velocity";
                apiResponse.Result = null;
                apiResponse.Code = (int)HttpStatusCode.InternalServerError;
                return StatusCode(apiResponse.Code, apiResponse);
            }
            apiResponse.Flag = Flag.Fail;
            apiResponse.Message = "No data to update";
            apiResponse.Result = null;
            apiResponse.Code = (int)HttpStatusCode.InternalServerError;
            return StatusCode(apiResponse.Code, apiResponse);

        }

        
    }
}
