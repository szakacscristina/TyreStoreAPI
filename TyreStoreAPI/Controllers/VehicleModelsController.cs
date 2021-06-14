using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TyreStoreAPI.Models;

namespace TyreStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelsController : ControllerBase
    {
        private readonly tyresDBContext _context;

        public VehicleModelsController(tyresDBContext context)
        {
            _context = context;
        }

        // GET: api/VehicleModels
        [HttpGet, Route("GetVehicleModels")]
        public async Task<ActionResult<IEnumerable<VehicleModels>>> GetVehicleModels()
        {
            return await _context.VehicleModels.ToListAsync();
        }

        // GET: api/VehicleModelById/5
        [HttpPost, Route("GetVehicleModelById")]
        public async Task<ActionResult<VehicleModels>> GetVehicleModelById([FromBody] int id)
        {
            var models = await _context.VehicleModels.FindAsync(id);

            if (models == null)
            {
                return NotFound();
            }

            return models;
        }

        // DELETE: api/Tyres/5
        [HttpPost, Route("DeleteVehicleModelById")]
        public async Task<ActionResult<IEnumerable<VehicleModels>>> DeleteVehicleModelById([FromBody] int id)
        {
            var models = await _context.VehicleModels.FindAsync(id);
            if (models == null)
            {
                return NotFound();
            }

            _context.VehicleModels.Remove(models);
            await _context.SaveChangesAsync();

            return await _context.VehicleModels.ToListAsync();
        }

        // UPDATE: api/Tyres
        [HttpPost, Route("UpdateVehicleModel")]
        public async Task<ActionResult<IEnumerable<VehicleModels>>> UpdateVehicleModel([FromBody] Tyres tyre)
        {
            _context.Entry(tyre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelsExists(tyre.Id))
                {
                    return NotFound();
                }
                else
                    throw;

            }

            return await _context.VehicleModels.ToListAsync();
        }

        // CREATE:
        [HttpPost, Route("CreateVehicleModel")]
        public async Task<ActionResult<IEnumerable<VehicleModels>>> CreateVehicleModel([FromBody] VehicleModels model)
        {

            model.Id = model.Id > 0 ? model.Id : _context.VehicleModels.ToList().Last().Id + 1;

            _context.VehicleModels.Add(model);
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tyres ON");

            await _context.SaveChangesAsync();

            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tyres OFF");

            return await _context.VehicleModels.ToListAsync();
        }

        private bool VehicleModelsExists(int id)
        {
            return _context.VehicleModels.Any(e => e.Id == id);
        }
    }
}
