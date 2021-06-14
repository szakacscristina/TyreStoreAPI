﻿using System;
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
    public class TyresSizesController : ControllerBase
    {
        private readonly tyresDBContext _context;

        public TyresSizesController(tyresDBContext context)
        {
            _context = context;
        }

        // GET: api/TyresSizes
        [HttpGet, Route("GetTyresSizes")]
        public async Task<ActionResult<IEnumerable<TyresSizes>>> GetTyresSizes()
        {
            return await _context.TyresSizes.ToListAsync();
        }

        // GET: api/Tyres/5
        [HttpPost, Route("GetTyresById")]
        public async Task<ActionResult<Tyres>> GetTyresById([FromBody] int id)
        {
            var tyres = await _context.Tyres.FindAsync(id);

            if (tyres == null)
            {
                return NotFound();
            }

            return tyres;
        }

        // DELETE: api/Tyres/5
        [HttpPost, Route("DeleteTyresById")]
        public async Task<ActionResult<IEnumerable<Tyres>>> DeleteTyresById([FromBody] int id)
        {
            var tyres = await _context.Tyres.FindAsync(id);
            if (tyres == null)
            {
                return NotFound();
            }

            _context.Tyres.Remove(tyres);
            await _context.SaveChangesAsync();

            return await _context.Tyres.ToListAsync();
        }

        // UPDATE: api/Tyres
        [HttpPost, Route("UpdateTyre")]
        public async Task<ActionResult<IEnumerable<Tyres>>> UpdateTyre([FromBody] Tyres tyre)
        {
            _context.Entry(tyre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!TyresSizesExists(tyre.Id))
                {
                    return NotFound();
                }
                else
                    throw;

            }

            return await _context.Tyres.ToListAsync();
        }

        // CREATE:
        [HttpPost, Route("CreateTyre")]
        public async Task<ActionResult<IEnumerable<Tyres>>> CreateTyre([FromBody] Tyres tyre)
        {

            tyre.Id = tyre.Id > 0 ? tyre.Id : _context.Tyres.ToList().Last().Id + 1;

            _context.Tyres.Add(tyre);
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tyres ON");

            await _context.SaveChangesAsync();

            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tyres OFF");

            return await _context.Tyres.ToListAsync();
        }
        private bool TyresSizesExists(int id)
        {
            return _context.TyresSizes.Any(e => e.Id == id);
        }
    }
}
