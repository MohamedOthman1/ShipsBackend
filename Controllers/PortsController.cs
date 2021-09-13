using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ShipsBackEnd.Models;
using ShipsBackEnd.Repositories;
using ShipsBackEnd.Utils;

namespace ShipsBackEnd.Controllers
{
    [ApiController]

    public class PortsController : ControllerBase
    {
        private readonly IPortsRepo _portsRepo;
        private readonly IShipsRepo _shipsRepo;

        public PortsController (IPortsRepo portsRepo, IShipsRepo shipsRepo) {
            _portsRepo = portsRepo;
            _shipsRepo = shipsRepo;
        }

        [HttpGet("ports/getAll")]
        public ActionResult<IEnumerable<Port>> GetAll()
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Flag = Flag.Pass;
            apiResponse.Message = "All Ports data successfully returned";
            apiResponse.Result = _portsRepo.GetAll();
            apiResponse.Code = (int)HttpStatusCode.OK;
            return StatusCode(apiResponse.Code, apiResponse);
        }

        [HttpGet("ports/getNearestPort/{id}")]
        public async Task<ActionResult<IEnumerable<Port>>> GetNearestShip(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (id > 0)
            {
                var ship = await  _shipsRepo.GetShip(id);
                if (ship != null)
                {
                    IList<Port> ports = _portsRepo.GetAll();
                    if (ports.Count > 0)
                    {
                        var portDetails = _portsRepo.GetNearestPort(ship,ports);
                        apiResponse.Flag = Flag.Pass;
                        apiResponse.Message = "Calculated Successfully";
                        apiResponse.Result = portDetails;
                        apiResponse.Code = (int)HttpStatusCode.OK;
                        return StatusCode(apiResponse.Code, apiResponse);
                    }
                    
                }
                
            }
            apiResponse.Flag = Flag.Fail;
            apiResponse.Message = "Failed to get nearest port to specific ship";
            apiResponse.Result = null;
            apiResponse.Code = (int) HttpStatusCode.InternalServerError;
            return StatusCode(apiResponse.Code, apiResponse);
        }

    }
}
