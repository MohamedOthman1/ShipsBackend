using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShipsBackEnd.Models;

namespace ShipsBackEnd.Repositories
{
    public class BaseRepo : IBaseRepo
    {
        private readonly DatabaseContext _context;

        public BaseRepo (DatabaseContext context) {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add (entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove (entity);
        }

        public async  Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync () > 0;
        }
    }
}
