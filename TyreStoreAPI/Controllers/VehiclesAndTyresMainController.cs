using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TyreStoreAPI.Models;

namespace Tyrestore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesAndTyresMainController : Controller

    {
        private readonly tyresDBContext _context;

        public VehiclesAndTyresMainController(tyresDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("GetVehicleModelsWithManufacturers")]

        public async Task<ActionResult<IEnumerable<VehicleModels>>>GetVehicleModelsWithManufacturers()
        {
            var joinedManufacturers = _context.VehicleModels.Join(
                _context.VehicleManufacturers,
                vehicleModel => vehicleModel.ManufacturerId,
                manufacturers => manufacturers.Id,
                (vehicleModel, manufacturers) => new VehicleModels
                {
                    Id = vehicleModel.Id,
                    Name = vehicleModel.Name,
                    ManufacturerId = vehicleModel.ManufacturerId,
                    Slug = vehicleModel.Slug,
                    StartYear = vehicleModel.StartYear,
                    EndYear = vehicleModel.EndYear,
                    Manufacturer = manufacturers

                });

            return await joinedManufacturers.ToListAsync();
        }

        [HttpPost, Route("GetTyreByVehicleModelId/{id?}")]

        public async Task<ActionResult<IEnumerable<Tyres>>> GetTyreByVehicleModelId([FromRoute] int id)
        {
            var tyreModel = _context.TyresModels.Where(x => x.ModelId == id);

            var joinedTyresSizes = _context.TyresModels.Join(
                _context.TyresSizes,
                model => model.TyreId,
                sizes => sizes.Id,
                (model, sizes) => sizes);
            var joinedTyres = _context.Tyres.Join(
                joinedTyresSizes,
                tyre => tyre.TyreId,
                tyreSizes => tyreSizes.Id,
                (tyre, tyreSizes) => new Tyres
                {
                    Id = tyre.Id,
                    Brand = tyre.Brand,
                    Season = tyre.Season,
                    Price = tyre.Price,
                    Stock = tyre.Stock,
                    TyreId = tyre.Id,
                    Tyre = tyreSizes
                }
            ).Distinct();


            return await joinedTyres.ToListAsync();
        }
    }
}