using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShipsBackEnd.Models;
using ShipsBackEnd.Utils;

namespace ShipsBackEnd.Repositories
{
    public class ShipsRepo : IShipsRepo
    {
        private readonly DatabaseContext _context;
        private readonly ILogHelper _logHelper;

        public ShipsRepo (ILogHelper logHelper, DatabaseContext context ) {
            _context = context;
            _logHelper = logHelper;
        }
        public async Task<Ship> GetShip (int id) {
            try
            {
                return await _context.Ships.FirstOrDefaultAsync (x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logHelper.ErrorLogger("Failed to retrieve this ship " + id, ex);
                return null;
            }

        }
        public  IList<Ship> GetAll () 
        {
            try
            {
                return _context.Ships.ToList();
            }
            catch (Exception ex)
            {
                _logHelper.ErrorLogger("Failed to retrieve all ships", ex);
                return null;
            }
        }

    }
}
